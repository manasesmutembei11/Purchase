using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface IRepositoryManager 
    {
        ICategoryRepository Category { get; }
        ICustomerRepository Customer { get; }

        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IOrderItemRepository OrderItem { get; }

        ITaxRepository Tax { get; }
        void Save();
    }
}
