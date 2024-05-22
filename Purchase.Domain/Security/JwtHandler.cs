using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Purchase.Domain.Contracts;
using Purchase.Domain.Managers;
using Purchase.Domain.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Security
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IRepositoryManager _repository;
        private readonly IConfigurationSection _jwtSettings;
        public JwtHandler(IConfiguration configuration, ApplicationUserManager userManager, ApplicationRoleManager roleManager, IRepositoryManager repository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
                            {
                                new Claim("username", user.Email),
                                new Claim("firstName", user.FirstName),
                                new Claim("lastName", user.LastName),
                                new Claim("email", user.Email),
                                new Claim("userId", user.Id.ToString())
                            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }
            var userclaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in userclaims)
            {

                if (claim.Type == CustomClaimTypes.Account && Guid.TryParse(claim.Value, out Guid accountId))
                {
                    claims.Add(new Claim(CustomClaimTypes.Account, accountId.ToString()));
                    //var warehouse = await _repository.Warehouse.GetByIdAsync(warehouseId);
                    //if (warehouse != null)
                    //{
                    //    claims.Add(new Claim(CustomClaimTypes.WarehouseId, warehouseId.ToString()));
                    //    if (warehouse.CountyId.HasValue)
                    //        claims.Add(new Claim(CustomClaimTypes.CountyId, warehouse.CountyId.Value.ToString()));
                    //    if (warehouse.MagisterialAreaId.HasValue)
                    //        claims.Add(new Claim(CustomClaimTypes.MagisterialAreaId, warehouse.MagisterialAreaId.Value.ToString()));
                    //}
                }

            }

            List<Claim> roleClaims = await GetRoleClaimsAsync(roles);
            return claims.Union(roleClaims).ToList();
        }

        private async Task<List<Claim>> GetRoleClaimsAsync(IList<string> roles)
        {

            var roleClaims = new List<Claim>();
            foreach (var r in roles)
            {
                var role = await _roleManager.FindByNameAsync(r);
                var claims = await _roleManager.GetClaimsAsync(role);
                roleClaims.AddRange(claims);
            }
            return roleClaims;

        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}
