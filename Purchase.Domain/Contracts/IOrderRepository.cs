using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
        void CreateOrder(Order order);

    }
}
