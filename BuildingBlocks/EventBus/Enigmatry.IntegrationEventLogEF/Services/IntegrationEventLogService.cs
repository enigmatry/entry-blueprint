using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.BuildingBlocks.EventBus.Events;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.BuildingBlocks.IntegrationEventLogEF.Services
{
    [UsedImplicitly]
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly DbContext _context;
        private readonly DbSet<IntegrationEventLogEntry> _dbSet;

        public IntegrationEventLogService(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<IntegrationEventLogEntry>();
        }

        public Task SaveEventAsync(IntegrationEvent @event)
        {
            var eventLogEntry = new IntegrationEventLogEntry(@event);

            _dbSet.Add(eventLogEntry);

            return _context.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(IntegrationEvent @event)
        {
            IntegrationEventLogEntry eventLogEntry = _dbSet.Single(ie => ie.EventId == @event.Id);
            eventLogEntry.TimesSent++;
            eventLogEntry.State = EventState.Published;

            _dbSet.Update(eventLogEntry);

            return _context.SaveChangesAsync();
        }
    }
}
