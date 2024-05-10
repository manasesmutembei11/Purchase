using AutoMapper;
using Purchase.Domain.Contracts;
using Purchase.Domain.IService;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Services
{
    //public sealed class ServiceManager : IServiceManager
    //{
    //    //private readonly Lazy<ICustomerService> _customerService;
    //    //private readonly Lazy<IOrderItemService> _orderItemService;
    //    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
    //    logger, IMapper mapper)
    //    {
    //        //_customerService = new Lazy<ICustomerService>(() => new
    //        //CustomerService(repositoryManager, logger, mapper));


    //        _orderItemService = new Lazy<IOrderItemService>(() => new
    //      OrderItemService(repositoryManager, logger, mapper));


    //    }
    //    public ICustomerService CustomerService => _customerService.Value;

    //    public IOrderItemService OrderItemService => _orderItemService.Value;
      
    //}
}