using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Purchase.Domain.DTOs.UserDTOs;
using Purchase.Domain.IEmail;
using Purchase.Domain.Managers;
using Purchase.Domain.Models.Templates;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Security;
using Purchase.Domain.Utilities;
using Purchase.Domain.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Purchase.Presentation.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailFactory _emailFactory;

        public AuthController
            (
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            JwtHandler jwtHandler,
            IMapper mapper,
            IEmailSender emailSender,
            ILogger<AuthController> logger,
            IEmailFactory emailFactory
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
            _emailFactory = emailFactory;
            var appBaseUrl = MyHttpContext.AppBaseUrl;
            _logger.LogDebug($"Base url => {appBaseUrl}");
            // var json= JsonConvert.SerializeObject(new AuthenticationDTO{ Email="juvegitau@yahoo.com",Password="$Exp@q2022" },Formatting.Indented);

        }
        [HttpPost("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] AuthenticationDTO userForAuthentication, bool? useCookies = false)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                var response = new BasicResponse();
                response.Message = "Invalid Authentication";
                response.AddError(0, "Invalid username or password");
                return Unauthorized(response);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var response = new BasicResponse();
                response.Message = "Error";
                response.AddError(0, "Email is not confirmed");
                return Unauthorized(response);
            }

            if (user != null && !user.Active)
            {
                var response = new BasicResponse();
                response.Message = "Invalid Authentication";
                response.AddError(0, "Invalid Authentication. Please contact system admin");
                return Unauthorized(response);
            }
            var claims = await _jwtHandler.GetClaimsAsync(user);
            if (useCookies.HasValue && useCookies.Value)
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Ok("Successfully logged in.");
            }
            else
            {
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponseDTO { Token = token });
            }

        }
        [HttpPost("Registration")]
        [ValidateModel]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            var response = new BasicResponse();
            if (registerUserDTO == null || !ModelState.IsValid)
            {
                response.Message = "Register User";
                response.AddError(0, "Invalid model state");
                return BadRequest(response);
            }

            var user = _mapper.Map<User>(registerUserDTO);
            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (!result.Succeeded)
            {

                response.Message = "User registration";
                var errors = result.Errors.ToList();
                errors.ForEach(f => response.AddError(0, f.Description));
                return BadRequest(response);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string> {
                { "token", token },
                { "email", user.Email }
            };
            var url = $"{MyHttpContext.AppBaseUrl}/auth/emailconfirmation";
            var callback = QueryHelpers.AddQueryString(url, param);
            var html = string.Format($"<a href=\"{callback}\">Confirm Email</a>");
            var message = new Message(new string[] { user.Email }, "Email Confirmation token", html);
            await _emailSender.SendEmailAsync(message);
            await _userManager.AddToRoleAsync(user, "user");

            response = new BasicResponse() { Message = "Ok" };
            return StatusCode(201, response);
        }





        [HttpGet("permissions")]
        [ValidateModel]
        public async Task<IActionResult> GetPermissions()
        {
            var userRole = await _roleManager.FindByNameAsync("admin");
            IList<Claim> alreadyIn = await _roleManager.GetClaimsAsync(userRole);
            foreach (Claim claim in alreadyIn)
            {
                await _roleManager.RemoveClaimAsync(userRole, claim);
            }
            foreach (var cp in RolePermissions.ClaimPermissions)
            {

                await _roleManager.AddClaimAsync(userRole, new Claim(cp.ClaimType, cp.Permission));
            }
            // IList<Claim> permission = await _roleManager.GetClaimsAsync(userRole);
            var response = new BasicResponse<IList<Claim>>();
            // response.AddData(permission);

            return Ok(response);
        }
        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            await SendPasswordResetEmail(user, "Set Password");
            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDto)
        {
            try
            {


                var response = new BasicResponse();
                if (!ModelState.IsValid)
                {
                    response.Message = "ForgotPassword";
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (user == null)
                {
                    response.Message = "ForgotPassword";
                    response.AddError(0, "Invalid Request");
                    return BadRequest(response);
                }
                await SendPasswordResetEmail(user, "Reset Password");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task SendPasswordResetEmail(User user, string subject)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>{
                {"token", token },
                {"email", user.Email }
            };
            var url = $"{MyHttpContext.AppBaseUrl}/account/reset-password";
            var callback = QueryHelpers.AddQueryString(url, param);
            var dataHolder = new Dictionary<string, string>();
            dataHolder.Add("{callback}", callback);
            dataHolder.Add("{FirstName}", user.FirstName);
            dataHolder.Add("{action}", subject);
            var html = _emailFactory.CreateEmail(EmailTemplateType.ResetPassword, dataHolder);
            var message = new Message(new string[] { user.Email }, subject, html);
            await _emailSender.SendEmailAsync(message);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            var response = new BasicResponse();
            if (!ModelState.IsValid)
            {
                response.Message = "ResetPassword";
                response.AddError(0, "Invalid model state");
                return BadRequest(response);
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                response.Message = "ResetPassword";
                response.AddError(0, "Invalid Request");
                return BadRequest(response);
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {

                response.Message = "ResetPassword";
                var errors = resetPassResult.Errors.ToList();
                errors.ForEach(f => response.AddError(0, f.Description));
                return BadRequest(response);

            }
            return Ok();
        }
        [HttpPost("Setup")]

        public async Task<IActionResult> Setup(SetupUserDTO dto)
        {
            var response = new BasicResponse();

            if (!_userManager.Users.Any())
            {


                var user = new User();
                user.Id = Guid.NewGuid();
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Active = true;
                user.Email = dto.Email;
                user.UserName = dto.Email;
                user.PhoneNumber = "+254722000000";
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                    var userRole = await _roleManager.FindByNameAsync("admin");
                    IList<Claim> alreadyIn = await _roleManager.GetClaimsAsync(userRole);
                    foreach (Claim claim in alreadyIn)
                    {
                        await _roleManager.RemoveClaimAsync(userRole, claim);
                    }
                    foreach (var cp in RolePermissions.ClaimPermissions)
                    {

                        await _roleManager.AddClaimAsync(userRole, new Claim(cp.ClaimType, cp.Permission));
                    }
                }
            }

            response = new BasicResponse() { Message = "Ok" };
            return StatusCode(201, response);
        }

    }

}
