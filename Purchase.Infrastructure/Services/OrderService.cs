using Purchase.Domain.Contracts;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Services
{
    internal sealed class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public OrderService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public IEnumerable<Order> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            try
            {
                var orders =
                _repository.Order.GetPagedListAsync(pagingParameters, trackChanges);
                return (IEnumerable<Order>) orders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedListAsync)} service method {ex}");
                throw;
            }
        }
    }
}
