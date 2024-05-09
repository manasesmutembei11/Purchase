using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ITaxRepository> _taxRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _customerRepository = new Lazy<ICustomerRepository>(() => new
            CustomerRepository(repositoryContext));

            _orderRepository = new Lazy<IOrderRepository>(() => new
            OrderRepository(repositoryContext));


            _productRepository = new Lazy<IProductRepository>(() => new
            ProductRepository(repositoryContext));


            _orderItemRepository = new Lazy<IOrderItemRepository>(() => new
            OrderItemRepository(repositoryContext));


            _categoryRepository = new Lazy<ICategoryRepository>(() => new
            CategoryRepository(repositoryContext));


            _taxRepository = new Lazy<ITaxRepository>(() => new
            TaxRepository(repositoryContext));
        }
        public ICustomerRepository Customer => _customerRepository.Value;
        public IOrderRepository Order => _orderRepository.Value;

        public IOrderItemRepository OrderItem => _orderItemRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public ITaxRepository Tax => _taxRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;
        public Task SaveAsync()
        {

            return _repositoryContext.SaveChangesAsync();
        }
    }

}
