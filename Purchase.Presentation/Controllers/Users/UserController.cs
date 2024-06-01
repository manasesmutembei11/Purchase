using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Contracts;
using Purchase.Domain.DTOs.UserDTOs;
using Purchase.Domain.IEmail;
using Purchase.Domain.Managers;
using Purchase.Domain.Models.Templates;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Paging;
using Purchase.Domain.Utilities;
using Purchase.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Presentation.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IMapper _mapper;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailFactory _emailFactory;
        private readonly IRepositoryManager _repository;

        public UserController(ILogger<UserController> logger,
            ApplicationUserManager applicationUserManager,
            IMapper mapper,
            ApplicationRoleManager roleManager,
            IEmailSender emailSender,
             IEmailFactory emailFactory,
             IRepositoryManager repository
            )
        {
            _logger = logger;
            _applicationUserManager = applicationUserManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _emailFactory = emailFactory;
            _repository = repository;
        }
        [HttpGet("{id}")]
        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> GetById(Guid id)
        {


            try
            {
                var entity = await _applicationUserManager.FindByIdAsync(id.ToString());
                var user = _mapper.Map<UserDTO>(entity);
                var userRoles = await _applicationUserManager.GetRolesAsync(entity);
                var roles = _roleManager.Roles.ToList();
                foreach (var role in roles)
                {
                    user.Roles.Add(new UserRoleDTO { Id = role.Id, Name = role.Name, Checked = userRoles.Contains(role.Name) });
                }
                var claims = await _applicationUserManager.GetClaimsAsync(entity);
                foreach (var claim in claims)
                {
                    if (claim.Type == CustomClaimTypes.OrganizationId && Guid.TryParse(claim.Value, out Guid organizationId))
                    {
                        user.OrganizationId = organizationId;
                    }
                    if (claim.Type == CustomClaimTypes.Account && Guid.TryParse(claim.Value, out Guid accountId))
                    {

                        var cc = await _repository.Account.GetByIdAsync(accountId);
                        if (cc != null)
                        {
                           // user.Accounts.Add(new AccountRefDTO { Id = accountId, Name = cc.Name, TypeName = cc.AccountType.ToDescription() });
                        }

                    }


                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }



        [HttpGet("users")]
        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> GetUsers([FromQuery] PagingParameters pagingParameters)
        {

            var data = await Task.Run(async () =>
            {
                var paged = await _applicationUserManager.GetPagedUsersAsync(pagingParameters, false);

                return new PagedList<UserDTO>(
                    paged.Data.Select(s => _mapper.Map<UserDTO>(s)).ToList(),
                    paged.MetaData.TotalCount,
                    paged.MetaData.CurrentPage,
                    paged.MetaData.PageSize);
            });

            return Ok(data);

        }

        [HttpPost("Save")]
        [ValidateModel]
        [Authorize(Permissions.Users.Edit)]
        public async Task<IActionResult> Save([FromBody] UserDTO dto)
        {
            var response = new BasicResponse();
            try
            {
                response.Message = "User Save";
                if (dto == null || !ModelState.IsValid)
                {
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var current = await _applicationUserManager.FindByIdAsync(dto.Id.ToString());
                if (current is null)
                {
                    var user = _mapper.Map<User>(dto);
                    var result = await _applicationUserManager.CreateAsync(user, "$Nec@2022");
                    if (!result.Succeeded)
                    {
                        response.Message = "User Creation";
                        var errors = result.Errors.ToList();
                        errors.ForEach(f => response.AddError(0, f.Description));
                        return BadRequest(response);
                    }
                    current = await _applicationUserManager.FindByIdAsync(dto.Id.ToString());


                    var token = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(user);
                    var param = new Dictionary<string, string> { { "token", token }, { "email", user.Email } };
                    var url = $"{MyHttpContext.AppBaseUrl}/account/confirm-email";
                    var callback = QueryHelpers.AddQueryString(url, param);
                    var dataHolder = new Dictionary<string, string>();
                    dataHolder.Add("{callback}", callback);
                    dataHolder.Add("{firstName}", current.FirstName);
                    var html = _emailFactory.CreateEmail(EmailTemplateType.EmailConfirmation, dataHolder);
                    var message = new Message(new string[] { user.Email }, "Email Confirmation token", html);
                    await _emailSender.SendEmailAsync(message);

                }
                else
                {
                    current.Email = dto.Email;
                    current.FirstName = dto.FirstName;
                    current.LastName = dto.LastName;
                    current.PhoneNumber = dto.PhoneNumber;
                    current.Active = dto.Active;
                    await _applicationUserManager.UpdateAsync(current);
                    var currentRoles = await _applicationUserManager.GetRolesAsync(current);
                    var roleToRemove = dto.Roles.Where(s => currentRoles.Contains(s.Name) && !s.Checked).Select(s => s.Name).ToList();
                    if (roleToRemove.Any())
                    {
                        await _applicationUserManager.RemoveFromRolesAsync(current, roleToRemove);
                    }
                    var claims = await _applicationUserManager.GetClaimsAsync(current);

                    //if (claims.Any(s => s.Type == CustomClaimTypes.DutyStationId))
                    //{
                    //    var claim = claims.Where(s => s.Type == CustomClaimTypes.DutyStationId);
                    //    await _applicationUserManager.RemoveClaimsAsync(current, claim);
                    //}
                    if (claims.Any(s => s.Type == CustomClaimTypes.Account))
                    {
                        var claim = claims.Where(s => s.Type == CustomClaimTypes.Account);
                        await _applicationUserManager.RemoveClaimsAsync(current, claim);
                    }
                }

                if (dto.OrganizationId.HasValue)
                {
                    await _applicationUserManager.AddClaimAsync(current, new Claim(CustomClaimTypes.OrganizationId, dto.OrganizationId.Value.ToString()));
                }
                foreach (var role in dto.Roles.Where(s => s.Checked))
                {
                    await _applicationUserManager.AddToRoleAsync(current, role.Name);
                }
                foreach (var account in dto.Accounts)
                {
                    await _applicationUserManager.AddClaimAsync(current, new Claim(CustomClaimTypes.Account, account.Id.ToString()));
                }
                response.Message = "OK";
                return StatusCode(201, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddError(0, ex.Message);
                response.Message = "OK";
                return StatusCode(500, response);
            }



        }


        [HttpPost("Activate")]
        [ValidateModel]
        [Authorize(Permissions.Users.Activate)]
        public async Task<IActionResult> Activate([FromBody] UserDTO dto)
        {
            var response = new BasicResponse();
            try
            {
                if (dto == null || !ModelState.IsValid)
                {
                    response.Message = "activate User";
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var user = await _applicationUserManager.FindByIdAsync(dto.Id.ToString());
                if (user == null)
                {
                    response.Message = "Activate User";
                    response.AddError(0, "invalid user");
                    return BadRequest(response);
                }
                user.Active = true;
                var result = await _applicationUserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {

                    response.Message = "User Activation";
                    var errors = result.Errors.ToList();
                    errors.ForEach(f => response.AddError(0, f.Description));
                    return BadRequest(response);
                }

                response = new BasicResponse() { Message = "Ok" };
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddError(0, ex.Message);
                response.Message = "OK";
                return StatusCode(500, response);

            }
        }
        [HttpPost("ResendEmailConfirmation")]
        [ValidateModel]

        public async Task<IActionResult> ResendEmailConfirmation([FromBody] UserDTO dto)
        {
            var response = new BasicResponse();
            try
            {

                if (dto == null || !ModelState.IsValid)
                {
                    response.Message = "activate User";
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var user = await _applicationUserManager.FindByIdAsync(dto.Id.ToString());
                if (user == null)
                {
                    response.Message = "Activate User";
                    response.AddError(0, "invalid user");
                    return BadRequest(response);
                }
                var token = await _applicationUserManager.GenerateEmailConfirmationTokenAsync(user);
                var param = new Dictionary<string, string> {
                { "token", token },
                { "email", user.Email }
            };
                var url = $"{MyHttpContext.AppBaseUrl}/account/confirm-email";
                var callback = QueryHelpers.AddQueryString(url, param);
                var dataHolder = new Dictionary<string, string>();
                dataHolder.Add("{callback}", callback);
                dataHolder.Add("{firstName}", dto.FirstName);
                var html = _emailFactory.CreateEmail(EmailTemplateType.EmailConfirmation, dataHolder);
                var message = new Message(new string[] { user.Email }, "Email Confirmation token", html);
                await _emailSender.SendEmailAsync(message);

                response = new BasicResponse() { Message = "Ok" };
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddError(0, ex.Message);
                response.Message = "OK";
                return StatusCode(500, response);

            }
        }
    }
}
