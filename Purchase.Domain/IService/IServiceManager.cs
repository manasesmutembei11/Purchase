using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IService
{
    public interface IServiceManager
    {
        ICustomerService CustomerService { get; }
        IOrderService OrderService { get; }
        IOrderItemService OrderItemService { get; }
       // IProductService ProductService { get; }
        ICategoryService CategoryService { get; }
    }
}
