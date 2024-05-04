using Purchase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;

namespace Purchase.Infrastructure.Services
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CustomerService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public IEnumerable<Customer> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            try
            {
                var customers =
                _repository.Customer.GetPagedListAsync(pagingParameters, trackChanges);
                return (IEnumerable<Customer>)customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the { nameof(GetPagedListAsync)} service method { ex}");
            throw;
            }
        }
    }

}
