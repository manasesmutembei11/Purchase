using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface IOrderItemRepository : IRepositoryBase<OrderItem, Guid>
    {
        Task<PagedList<OrderItem>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
      

    }
}
