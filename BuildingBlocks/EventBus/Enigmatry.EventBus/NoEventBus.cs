using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.BuildingBlocks.EventBus
{
    public class NoEventBus : IEventBus
    {
        public void Publish(IntegrationEvent @event)
        {
            
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            
        }
    }
}
