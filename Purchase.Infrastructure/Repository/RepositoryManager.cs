using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Caching;
using Purchase.Domain.Contracts;
using Purchase.Domain.Contracts.Configs;
using Purchase.Domain.Contracts.Counters;
using Purchase.Infrastructure.Repository.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    /*
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
    */
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ILogger<RepositoryManager> _logger;
        private readonly RepositoryContext _context;
    
        private readonly Lazy<IConfigRepository> _configRepository;
        private readonly Lazy<IAppCounterRepository> _appCounterRepository;
        private readonly Lazy<ITaxRepository> _taxRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        public RepositoryManager(
            ILogger<RepositoryManager> logger,
            RepositoryContext context,
            Lazy<IConfigRepository> configRepository,
            Lazy<IAppCounterRepository> appCounterRepository,
            Lazy<ITaxRepository> taxRepository,
            Lazy<ICategoryRepository> categoryRepository,
            Lazy<IOrderRepository> orderRepository,
            Lazy<IOrderItemRepository> orderItemRepository,
            Lazy<IUserRepository> userRepository,
            Lazy<IProductRepository> productRepository,
            Lazy<ICustomerRepository> customerRepository,

            Lazy<IAccountRepository> accountRepository
            )
        {
            _logger = logger;
            _context = context;
            _configRepository = configRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _taxRepository = taxRepository;
            _categoryRepository = categoryRepository;
            _appCounterRepository = appCounterRepository;
        }
        public IConfigRepository Config => _configRepository.Value;
        public IAppCounterRepository AppCounter => _appCounterRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public ICustomerRepository Customer => _customerRepository.Value;
        public IOrderRepository Order => _orderRepository.Value;
        public IOrderItemRepository OrderItem => _orderItemRepository.Value;

        public ITaxRepository Tax => _taxRepository.Value;
        public IAccountRepository Account => _accountRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public IProductRepository Product => _productRepository.Value;
 

        public Task SaveAsync()
        {
            _logger.LogDebug("SaveAsync");
            return _context.SaveChangesAsync();
        }


    }

}
