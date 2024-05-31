using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Models;

namespace Purchase.Domain.Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account, Guid>
    {
        Task<PagedList<Account>> GetPagedListAsync(Expression<Func<Account, bool>> expression, PagingParameters pagingParameters, bool trackChanges);
    }
}
