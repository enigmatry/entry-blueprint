using Autofac;
using Enigmatry.Blueprint.Core.Settings;
using Microsoft.Extensions.Configuration;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => c.Resolve<IConfiguration>().AppSettings())
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().ServiceBus).As<ServiceBusSettings>()
                .SingleInstance();
        }
    }
}