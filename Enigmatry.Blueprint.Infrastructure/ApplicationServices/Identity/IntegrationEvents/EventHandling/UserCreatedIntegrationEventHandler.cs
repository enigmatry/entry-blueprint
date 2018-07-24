using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    public class UserCreatedIntegrationEventHandler: IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private ILogger<UserCreatedIntegrationEventHandler> _logger;

        public UserCreatedIntegrationEventHandler(ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event. {UserId}", @event.UserName);
            return Task.CompletedTask;
        }
    }
}
