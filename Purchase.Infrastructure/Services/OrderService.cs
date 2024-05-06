using Purchase.Domain.Contracts;
using Purchase.Domain.DTOs;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public OrderService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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

        public OrderDTO CreateOrder(OrderDTO order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            _repository.Order.CreateOrder(orderEntity);
            _repository.Save();
            var orderToReturn = _mapper.Map<OrderDTO>(orderEntity);
            return orderToReturn;
        }

        public OrderDTO GetOrder(Guid id, bool trackChanges)
        {
            var order = _repository.Order.GetOrder(id, trackChanges);

            var orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
        }


        public void DeleteOrder(Guid Id, bool trackChanges)
        {
            var order = _repository.Order.GetOrder(Id, trackChanges);
            if (order is null)
                throw null;

            _repository.Order.DeleteOrder(order);
            _repository.Save();
        }
    }
}
