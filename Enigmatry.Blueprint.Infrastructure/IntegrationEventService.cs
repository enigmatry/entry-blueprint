using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.EventBus;
using Enigmatry.BuildingBlocks.EventBus.Abstractions;
using Enigmatry.BuildingBlocks.EventBus.Events;
using Enigmatry.BuildingBlocks.IntegrationEventLogEF.Services;
using JetBrains.Annotations;

namespace Enigmatry.Blueprint.Infrastructure
{
    [UsedImplicitly]
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly IIntegrationEventLogService _eventLogService;

        public IntegrationEventService(IEventBus eventBus, IIntegrationEventLogService integrationEventLogService)
        {
            _eventBus = eventBus;
            _eventLogService = integrationEventLogService;
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            await SaveEventAndTxfContextChangesAsync(evt);
            _eventBus.Publish(evt);
            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        private async Task SaveEventAndTxfContextChangesAsync(IntegrationEvent evt)
        {
            await _eventLogService.SaveEventAsync(evt);
        }
    }
}