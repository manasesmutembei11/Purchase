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
    internal sealed class OrderItemService : IOrderItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public OrderItemService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<OrderItem> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            try
            {
                var orderItems =
                _repository.OrderItem.GetPagedListAsync(pagingParameters, trackChanges);
                return (IEnumerable<OrderItem>) orderItems;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedListAsync)} service method {ex}");
                throw;
            }
        }

        public OrderItemDTO CreateOrderItem(OrderItemDTO orderItem)
        {
            var orderItemEntity = _mapper.Map<OrderItem>(orderItem);
            _repository.OrderItem.CreateOrderItem(orderItemEntity);
            _repository.SaveAsync();
            var orderItemToReturn = _mapper.Map<OrderItemDTO>(orderItemEntity);
            return orderItemToReturn;
        }

        public OrderItemDTO GetOrderItem(Guid id, bool trackChanges)
        {
            var orderItem = _repository.OrderItem.GetOrderItem(id, trackChanges);

            var orderItemDto = _mapper.Map<OrderItemDTO>(orderItem);
            return orderItemDto;
        }


        public void DeleteOrderItem(Guid Id, bool trackChanges)
        {
            var orderItem = _repository.OrderItem.GetOrderItem(Id, trackChanges);
            if (orderItem is null)
                throw null;

            _repository.OrderItem.DeleteOrderItem(orderItem);
            _repository.SaveAsync();
        }
    }
}
