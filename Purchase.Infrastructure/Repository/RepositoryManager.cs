using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Caching;
using Purchase.Domain.Contracts;
using Purchase.Domain.Contracts.Configs;
using Purchase.Infrastructure.Repository.Configs;
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
        private readonly ICacheProvider _cacheProvider;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ITaxRepository> _taxRepository;
        private readonly Lazy<IConfigRepository> _configRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        public RepositoryManager(RepositoryContext repositoryContext, ICacheProvider cacheProvider)
        {
            _repositoryContext = repositoryContext;
            _cacheProvider = cacheProvider;

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

            _accountRepository = new Lazy<IAccountRepository>(() => new
            AccountRepository(repositoryContext));

            _configRepository = new Lazy<IConfigRepository>(() => new
            ConfigRepository(repositoryContext, cacheProvider));
        }
        public ICustomerRepository Customer => _customerRepository.Value;
        public IOrderRepository Order => _orderRepository.Value;

        public IOrderItemRepository OrderItem => _orderItemRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public ITaxRepository Tax => _taxRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;
        public IConfigRepository Config => _configRepository.Value;
        public IAccountRepository Account => _accountRepository.Value;
        public Task SaveAsync()
        {

            return _repositoryContext.SaveChangesAsync();
        }
    }

}
