using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.BuildingBlocks.EventBus;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<UserCreatedDomainEventHandler> _log;

        public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> log,
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