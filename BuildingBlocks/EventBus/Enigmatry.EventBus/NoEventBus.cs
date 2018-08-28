using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.BuildingBlocks.EventBus
{
    public class NoEventBus : IEventBus
    {
        public void Publish(IntegrationEvent @event)
        {
            // does nothing
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            // does nothing
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            // does nothing
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            // does nothing
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            // does nothing
        }
    }
}
