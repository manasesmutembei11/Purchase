using Purchase.Domain.Contracts;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    internal class AccountRepository : RepositoryBase<Account, Guid>, IAccountRepository
    {
        public AccountRepository(RepositoryContext context) : base(context)
        {
        }
        public Task<PagedList<Account>> GetPagedListAsync(Expression<Func<Account, bool>> expression, PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindByCondition(expression, trackChanges).OrderBy(e => e.Code);
            return PagedList<Account>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public override void Delete(Account item)
        {
            var entity = GetById(item.Id);
            if (entity != null)
            {

                entity.Status = EntityStatus.Deleted;
            }

        }
    }
}
