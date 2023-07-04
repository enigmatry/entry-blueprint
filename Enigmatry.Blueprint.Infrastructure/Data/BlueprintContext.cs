using Enigmatry.Entry.Core;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.EntityFramework.Security;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Enigmatry.Entry.EntityFramework.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Enigmatry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class BlueprintContext : MediatRDbContext
{
    private readonly ITimeProvider _timeProvider;
    private readonly ICurrentUserIdProvider _currentUserIdProvider;

    public BlueprintContext(DbContextOptions options,
        IMediator mediator,
        ITimeProvider timeProvider,
        ICurrentUserIdProvider currentUserIdProvider,
        IDbContextAccessTokenProvider dbContextAccessTokenProvider,
        ILogger<BlueprintContext> logger) :
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
                var currentDateTime = _timeProvider.FixedUtcNow;
                if (state == EntityState.Added)
                {
                    entity.SetCreated(currentDateTime, userId.Value);
                    entity.SetUpdated(currentDateTime, userId.Value);
                }

                if (state == EntityState.Modified)
                {
                    entity.SetUpdated(currentDateTime, userId.Value);
                }
            }
        }
    }
}
