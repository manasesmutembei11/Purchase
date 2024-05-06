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
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
        OrderItemDTO CreateOrderItem(OrderItemDTO orderItem);
    }
}
