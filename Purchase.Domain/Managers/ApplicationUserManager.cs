using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Managers
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger
            ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<PagedList<User>> GetPagedUsersAsync(PagingParameters pagingParameters, bool trackChanges)
        {

            return await Task.Run(() =>
            {
                var query = Users;
                if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
                {
                    var search = pagingParameters.Search;
                    query = query.Where(s => s.UserName.Contains(search) || s.Email.Contains(search) || s.FirstName.Contains(search) || s.FirstName.Contains(search));
                }
                var roles = query.OrderBy(e => e.UserName).ToList();
                return PagedList<User>.ToPagedList(roles, pagingParameters.PageNumber, pagingParameters.PageSize);
            });

        }

    }
}
