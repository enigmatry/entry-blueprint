using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.EventBus.Events;

namespace Enigmatry.BuildingBlocks.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent @event);
        Task MarkEventAsPublishedAsync(IntegrationEvent @event);
    }
}
