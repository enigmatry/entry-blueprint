using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework.MediatR
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BlueprintContext ctx)
        {
            List<EntityEntry<Entity>> domainEntities = ctx.ChangeTracker
                .Entries<Entity>().Where(x => x.Entity.DomainEvents.Any()).ToList();

            List<INotification> domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            IEnumerable<Task> tasks = domainEvents
                .Select(async domainEvent => { await mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
        }
    }
}