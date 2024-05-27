using Purchase.Domain.Contracts;
using Purchase.Domain.Security;
using Autofac;

namespace Purchase.API.Extensions
{
    public class WebAppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHandler>();
            builder.RegisterType<AppUserProvider>().As<IUserProvider>();
            // builder.RegisterInstance<Channel<ProcessCardChannelMessage>>(Channel.CreateUnbounded<ProcessCardChannelMessage>(new UnboundedChannelOptions() { SingleReader = true }));
            //builder.Register<ChannelReader<ProcessCardChannelMessage>>(s => s.Resolve<Channel<ProcessCardChannelMessage>>().Reader).SingleInstance();
            // builder.Register<ChannelWriter<ProcessCardChannelMessage>>(s => s.Resolve<Channel<ProcessCardChannelMessage>>().Writer).SingleInstance();


        }
    }
}
