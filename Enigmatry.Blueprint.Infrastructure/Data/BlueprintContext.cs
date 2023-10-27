using Enigmatry.Entry.Core;
using Enigmatry.Entry.Core.Entities;
using Enigmatry.Entry.EntityFramework;
using Enigmatry.Entry.EntityFramework.Security;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Infrastructure.Api.Init;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Enigmatry.Entry.EntityFramework.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;
using Enigmatry.Blueprint.Domain.Identity;

namespace Enigmatry.Blueprint.Infrastructure.Data;

[UsedImplicitly]
public class BlueprintContext : MediatRDbContext
{
    private readonly ITimeProvider _timeProvider;
    // Injecting Func to avoid a circular dependency with DI between BlueprintContext and CurrentUserProvider.
    private readonly Func<ICurrentUserProvider> _currentUserProvider;

    public BlueprintContext(DbContextOptions options,
        IMediator mediator,
        ITimeProvider timeProvider,
        Func<ICurrentUserProvider> currentUserProvider,
        IDbContextAccessTokenProvider dbContextAccessTokenProvider,
        ILogger<BlueprintContext> logger) :
        base(CreateOptions(), options, mediator, logger, dbContextAccessTokenProvider)
    {
        _timeProvider = timeProvider;
        _currentUserProvider = currentUserProvider;
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
        var currentUserProvider = _currentUserProvider();

        var userId = currentUserProvider.IsAuthenticated
            ? currentUserProvider.User?.Id
            : null;

        var changedEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) &&
                        x.Entity is IEntityWithCreatedUpdated)
            .Select(x => (x.State, Entity: (IEntityWithCreatedUpdated)x.Entity)).ToList();

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
