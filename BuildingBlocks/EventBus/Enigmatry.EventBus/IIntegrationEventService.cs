using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.BuildingBlocks.EventBus
{
    public interface IIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}