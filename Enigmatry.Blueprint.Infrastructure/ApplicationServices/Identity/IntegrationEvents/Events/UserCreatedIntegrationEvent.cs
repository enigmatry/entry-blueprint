using System;
using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.Blueprint.Infrastructure.ApplicationServices.Identity
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(string userName)
        {
            UserName = userName;
        }

        public string UserName { get;  }
    }
}