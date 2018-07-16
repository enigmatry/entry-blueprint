using System.Linq;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using MediatR;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework.MediatR
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BlueprintContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents
                .Select(async domainEvent => { await mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
        }
    }
}