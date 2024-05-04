using Purchase.Domain.Contracts;
using Purchase.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IOrderItemService> _orderItemService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ITaxService> _taxService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger)
        {
            _customerService = new Lazy<ICustomerService>(() => new
            CustomerService(repositoryManager, logger));

            _orderService = new Lazy<IOrderService>(() => new
            OrderService(repositoryManager, logger));

            _orderItemService = new Lazy<IOrderItemService>(() => new
          OrderItemService(repositoryManager, logger));

            _productService = new Lazy<IProductService>(() => new
          ProductService(repositoryManager, logger));

            _categoryService = new Lazy<ICategoryService>(() => new
          CategoryService(repositoryManager, logger));

            _taxService = new Lazy<ITaxService>(() => new
          TaxService(repositoryManager, logger));

        }
        public ICustomerService CustomerService => _customerService.Value;
        public IOrderService OrderService => _orderService.Value;

        public IOrderItemService OrderItemService => _orderItemService.Value;
        public IProductService ProductService => _productService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public ITaxService TaxService => _taxService.Value;
    }
}