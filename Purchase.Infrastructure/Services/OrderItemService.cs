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
            _repository.Save();
            var orderItemToReturn = _mapper.Map<OrderItemDTO>(orderItemEntity);
            return orderItemToReturn;
        }
    }
}
