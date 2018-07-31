using Autofac;
using Enigmatry.Blueprint.Core.Settings;
using Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity;
using Enigmatry.BuildingBlocks.EventBus;
using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Enigmatry.BuildingBlocks.EventBusServiceBus;
using Enigmatry.BuildingBlocks.IntegrationEventLogEF.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class EventBusModule : Module
    {
        public bool AzureServiceBusEnabled { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (AzureServiceBusEnabled)
            {
                //TODO: this part has not been tested
                RegisterAzureServiceBusAsEventBus(builder);
            }
            else
            {
                RegisterNoEventBus(builder);
            }

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>()
                .As<IEventBusSubscriptionsManager>()
                .SingleInstance();

            RegisterIntegrationEventHandlers(builder);

            builder.RegisterType<IntegrationEventLogService>()
                .As<IIntegrationEventLogService>()
                .InstancePerLifetimeScope();
        }

        private static void RegisterAzureServiceBusAsEventBus(ContainerBuilder builder)
        {
            builder.Register(context =>
                {
                    var logger = context.Resolve<ILogger<DefaultServiceBusPersisterConnection>>();
                    var configuration = context.Resolve<ServiceBusSettings>();
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(configuration.EventBusConnection);
                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                }).As<IServiceBusPersisterConnection>()
                .SingleInstance();

            builder.Register(context =>
                {
                    var configuration = context.Resolve<ServiceBusSettings>();
                    string subscriptionClientName = configuration.SubscriptionClientName;
                    var serviceBusPersisterConnection = context.Resolve<IServiceBusPersisterConnection>();
                    var iLifetimeScope = context.Resolve<ILifetimeScope>();
                    var logger = context.Resolve<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = context.Resolve<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                }).As<IEventBus>()
                .SingleInstance();
        }

        private static void RegisterNoEventBus(ContainerBuilder builder)
        {
            builder.Register(c => new NoEventBus()).As<IEventBus>().SingleInstance();
        }

        private static void RegisterIntegrationEventHandlers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UserCreatedIntegrationEventHandler).Assembly)
                .Where(
                    type => type.Name.EndsWith("IntegrationEventHandler")
                ).AsSelf().InstancePerDependency();
            //TODO this needs to be checked
            //builder.RegisterType<UserCreatedIntegrationEventHandler>().AsSelf().InstancePerDependency();
        }
    }
}