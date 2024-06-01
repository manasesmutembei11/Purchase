using Purchase.Domain.Contracts;
using Purchase.Domain.IEmail;
using Purchase.Infrastructure.Email;
using Purchase.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Purchase.Infrastructure.Repository.Counters;
using Purchase.Infrastructure.Repository.Configs;
using Purchase.Infrastructure.Repository;
using Purchase.Domain.Contracts.Counters;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Purchase.Domain.Contracts.Configs;
using Purchase.Domain.Caching;
using Purchase.Infrastructure.Caching;
namespace Purchase.Infrastructure
{
    public class DataRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkdiXX5ecnJWRGNeV0U=");


            //reports
       
            builder.RegisterType<AppCounterRepository>().As<IAppCounterRepository>().WithParameter(
                new ResolvedParameter(
                (pi, cc) => pi.Name == "context",
                (pi, cc) => cc.Resolve<ILifetimeScope>().BeginLifetimeScope().Resolve<RepositoryContext>()
                ));
            //builder.RegisterType<ServiceForHandler>().InstancePerOwned<MessageHandler>();
            //services
         
            builder.RegisterType<EmailFactory>().As<IEmailFactory>();
            //caching
         
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<TaxRepository>().As<ITaxRepository>();
            builder.RegisterType<ConfigRepository>().As<IConfigRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
           // builder.RegisterType<AppCounterRepository>().As<IAppCounterRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ConfigRepository>().As<IConfigRepository>();
            builder.RegisterType<EmailSender>().As<IEmailSender>();
            builder.RegisterType<DefaultCacheProvider>().As<ICacheProvider>().SingleInstance();

            //manager
            builder.RegisterType<RepositoryManager>().As<IRepositoryManager>();

        }
    }
}
