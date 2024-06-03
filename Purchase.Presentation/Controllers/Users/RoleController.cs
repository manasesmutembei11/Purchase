using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Purchase.Domain.DTOs.UserDTOs;
using Purchase.Domain.Managers;
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
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Purchase.Presentation.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            ApplicationRoleManager roleManager,
            ILogger<RoleController> logger
            )
        {
            _roleManager = roleManager;
            _logger = logger;
            _logger.LogDebug("init");
        }
        [HttpGet("roles")]
        [Authorize(Policy = Permissions.Users.Roles)]
        public async Task<IActionResult> GetRoles([FromQuery] PagingParameters pagingParameters)
        {
            PagedList<Role> data = await _roleManager.GetPagedRolesAsync(pagingParameters, false);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _roleManager.FindByIdAsync(id.ToString());
            return Ok(data);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Users.Roles)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = new BasicResponse();
            try
            {
                string[] system_roles = new string[] { "admin", "user" };
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role != null && !system_roles.Contains(role.Name.ToLower()))
                {
                    await _roleManager.DeleteAsync(role);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.AddError(0, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("Save")]
        [ValidateModel]
        [Authorize(Policy = $"{Permissions.Users.Roles}")]
        public async Task<IActionResult> Save([FromBody] Role role)
        {
            var response = new BasicResponse();
            response.Message = "Add Role";
            if (role == null || !ModelState.IsValid)
            {
                response.AddError(0, "Invalid model state");
                return BadRequest(response);
            }
            IdentityResult result;
            string[] system_roles = new string[] { "admin", "user" };
            var r = await _roleManager.FindByIdAsync(role.Id.ToString());
            if (r == null)
            {
                result = await _roleManager.CreateAsync(new Role { Id = role.Id, Name = role.Name });
            }
            else if (!system_roles.Contains(r.Name))
            {
                r.Name = role.Name;
                result = await _roleManager.UpdateAsync(r);

            }
            else { result = null; }
            if (result != null && !result.Succeeded)
            {
                var errors = result.Errors.ToList();
                errors.ForEach(f => response.AddError(0, f.Description));
                return BadRequest(response);
            }
            response.Message = "OK";
            return StatusCode(201, response);
        }

        [HttpGet("permissions/{roleId}")]
        [Authorize(Policy = Permissions.Users.Roles)]
        public async Task<IActionResult> GetRolePermissions(Guid roleId)
        {
            var response = new BasicResponse();
            response.Message = "Permissions";
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                response.AddError(0, "Invalid role id");
                return BadRequest(response);
            }
            var model = new RolePermissionsDTO();
            model.RoleId = roleId;
            model.RoleName = role.Name;
            var claims = await _roleManager.GetClaimsAsync(role);
            var assigedPermission = claims.Where(s => s.Type == CustomClaimTypes.Permission).Select(s => s.Value.ToLower()).ToList();
            foreach (var m in RolePermissions.ClaimPermissions.GroupBy(s => s.Module))
            {
                var module = new RoleModulePermissionDTO() { Name = m.Key };
                model.RoleModules.Add(module);
                foreach (var p in m.ToList())
                {
                    var permission = new PermissionDTO() { Name = p.Permission, Description = p.Description };
                    if (assigedPermission.Contains(p.Permission.ToLower()))
                    {
                        permission.CanAccess = true;
                    }
                    module.Permissions.Add(permission);
                }
            }

            return Ok(model);
        }
        [HttpPost("SavePermissions")]
        [ValidateModel]
        [Authorize(Policy = Permissions.Users.Roles)]
        public async Task<IActionResult> SavePermissions([FromBody] RolePermissionsDTO rolePermissions)
        {
            var response = new BasicResponse();
            response.Message = "Permission";
            if (rolePermissions == null || !ModelState.IsValid)
            {
                response.AddError(0, "Invalid model state");
                return BadRequest(response);
            }
            var role = await _roleManager.FindByIdAsync(rolePermissions.RoleId.ToString());
            if (role == null)
            {
                response.AddError(0, "Invalid role id");
                return BadRequest(response);
            }

            IList<Claim> alreadyIn = await _roleManager.GetClaimsAsync(role);
            foreach (Claim claim in alreadyIn)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var assignedPermissions = rolePermissions.RoleModules.SelectMany(s => s.Permissions).Where(s => s.CanAccess).Select(s => s.Name).ToList();
            foreach (var cp in RolePermissions.ClaimPermissions)
            {

                if (assignedPermissions.Contains(cp.Permission))
                {
                    await _roleManager.AddClaimAsync(role, new Claim(cp.ClaimType, cp.Permission));
                }

            }
            response.Message = "OK";
            return StatusCode(201, response);
        }

        [HttpGet("lookuplist")]
        public async Task<IActionResult> GetLookupList()
        {
            var data = await _roleManager.Roles
                .OrderBy(s => s.Name).Select(s => new UserRoleDTO { Id = s.Id, Name = s.Name.Humanize(LetterCasing.Title), Checked = false }).ToListAsync();
            return Ok(data);
        }

    }
}
