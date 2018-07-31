using System.Threading.Tasks;
using Enigmatry.Blueprint.ApplicationServices.Identity.IntegrationEvents.Events;
using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.ApplicationServices.Identity.IntegrationEvents.EventHandling
{
    [UsedImplicitly]
    public class UserCreatedIntegrationEventHandler: IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;

        public UserCreatedIntegrationEventHandler(ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event. {UserId}", @event.UserName);
            // e.g. here we can call external api and store users
            return Task.CompletedTask;
        }
    }
}
