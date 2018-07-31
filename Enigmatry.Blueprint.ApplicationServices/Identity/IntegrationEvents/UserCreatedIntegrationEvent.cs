using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.Blueprint.ApplicationServices.Identity.IntegrationEvents.Events
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