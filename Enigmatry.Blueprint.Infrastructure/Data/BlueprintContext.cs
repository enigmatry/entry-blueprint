using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Enigmatry.Blueprint.BuildingBlocks.Core;
using Enigmatry.Blueprint.BuildingBlocks.Core.Entities;
using Enigmatry.Blueprint.BuildingBlocks.EntityFramework;
using Enigmatry.Blueprint.BuildingBlocks.EntityFramework.Security;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using Enigmatry.Blueprint.Model.Identity;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Data
{
    [UsedImplicitly]
    public class BlueprintContext : EntitiesDbContext
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ICurrentUserIdProvider _currentUserIdProvider;

        public BlueprintContext(DbContextOptions options, IMediator mediator,
            ITimeProvider timeProvider, ICurrentUserIdProvider currentUserIdProvider,
            ILogger<BlueprintContext> logger, IDbContextAccessTokenProvider dbContextAccessTokenProvider) :
            base(CreateOptions(), options, mediator, logger, dbContextAccessTokenProvider)
        {
            _timeProvider = timeProvider;
            _currentUserIdProvider = currentUserIdProvider;
        }

        private static EntitiesDbContextOptions CreateOptions() => new()
        {
            ConfigurationAssembly = AssemblyFinder.InfrastructureAssembly,
            EntitiesAssembly = AssemblyFinder.DomainAssembly
        };

        public override int SaveChanges()
        {
            var task = Task.Run(async () => await SaveChangesAsync());
            return task.GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PopulateCreatedUpdated();
            var saved = await base.SaveChangesAsync(cancellationToken);
            return saved;
        }

        private void PopulateCreatedUpdated()
        {
            var userId = _currentUserIdProvider.IsAuthenticated
                ? _currentUserIdProvider.UserId
                : null;

            var changedEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) &&
                            x.Entity is IEntityHasCreatedUpdated)
                .Select(x => (x.State, Entity: (IEntityHasCreatedUpdated)x.Entity)).ToList();

            if (userId.HasValue)
            {
                foreach (var (state, entity) in changedEntities)
                {
                    if (state == EntityState.Added)
                    {
                        entity.SetCreated(_timeProvider.Now, userId.Value);
                        entity.SetUpdated(_timeProvider.Now, userId.Value);
                    }

                    if (state == EntityState.Modified)
                    {
                        entity.SetUpdated(_timeProvider.Now, userId.Value);
                    }
                }
            }
        }
    }
}
