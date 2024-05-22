using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Managers
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        public ApplicationRoleManager(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger
            ) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
        public async Task<PagedList<Role>> GetPagedRolesAsync(PagingParameters pagingParameters, bool trackChanges)
        {

            return await Task.Run(() =>
            {
                var roles = Roles.OrderBy(e => e.Name).ToList();
                return PagedList<Role>.ToPagedList(roles, pagingParameters.PageNumber, pagingParameters.PageSize);
            });

        }
    }
}
