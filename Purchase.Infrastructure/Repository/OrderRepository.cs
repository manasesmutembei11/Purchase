using Purchase.Domain.Models;
using Purchase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Purchase.Domain.Paging;

namespace Purchase.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Order>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindAll(trackChanges).OrderBy(e => e.OrderDate);
            return PagedList<Order>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }

}
