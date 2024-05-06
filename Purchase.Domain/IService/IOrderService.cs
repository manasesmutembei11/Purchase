using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using Purchase.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IService
{
    public interface IOrderService
    {
        IEnumerable<Order> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
        OrderDTO CreateOrder(OrderDTO order);

        OrderDTO GetOrder(Guid id, bool trackChanges);

        void DeleteOrder(Guid id, bool trackChanges);
    }
}
