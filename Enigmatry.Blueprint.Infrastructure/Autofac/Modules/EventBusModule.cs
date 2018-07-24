using Autofac;
using Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity;
using Enigmatry.BuildingBlocks.EventBus;
using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Enigmatry.BuildingBlocks.IntegrationEventLogEF.Services;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateEventBus).As<IEventBus>().SingleInstance();

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>()
                .As<IEventBusSubscriptionsManager>()
                .SingleInstance();

            RegisterIntegrationEventHandlers(builder);

            builder.RegisterType<IntegrationEventLogService>()
                .As<IIntegrationEventLogService>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterIntegrationEventHandlers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UserCreatedIntegrationEventHandler).Assembly)
                .Where(
                    type => type.Name.EndsWith("IntegrationEventHandler")
                ).AsSelf().InstancePerDependency();
            //TODO register by name
            //builder.RegisterType<UserCreatedIntegrationEventHandler>().AsSelf().InstancePerDependency();
        }

        private IEventBus CreateEventBus(IComponentContext container)
        {
            // here you replace with implementation of RabbitMq or ServiceBus bus.
            return new NoEventBus();
        }
    }
}