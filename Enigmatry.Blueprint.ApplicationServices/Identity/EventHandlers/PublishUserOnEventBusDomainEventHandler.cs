using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.ApplicationServices.Identity.IntegrationEvents.Events;
using Enigmatry.Blueprint.Model.Identity.DomainEvents;
using Enigmatry.BuildingBlocks.EventBus;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.ApplicationServices.Identity.EventHandlers
{
    [UsedImplicitly]
    public class PublishUserOnEventBusDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<PublishUserOnEventBusDomainEventHandler> _log;

        public PublishUserOnEventBusDomainEventHandler(ILogger<PublishUserOnEventBusDomainEventHandler> log,
            IIntegrationEventService integrationEventService)
        {
            _log = log;
            _integrationEventService = integrationEventService;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var @event = new UserCreatedIntegrationEvent(notification.UserName);

            _log.LogDebug("User created: {UserName}", notification.UserName);

            await _integrationEventService.PublishThroughEventBusAsync(@event);
        }
    }
}
