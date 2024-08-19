using Enigmatry.Entry.Blueprint.Core.Entities;
using Enigmatry.Entry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Core;
using Enigmatry.Entry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Data;

public class PopulateCreatedUpdatedInterceptor : SaveChangesInterceptor
{
    private readonly ITimeProvider _timeProvider;

    // Injecting Func to avoid circular DI dependency between DbContext and CurrentUserProvider
    // CurrentUserProvider -> (depends on) IRepository<User> -> DbContext -> ISaveChangesInterceptors (PopulateCreatedUpdatedInterceptor) -> CurrentUserProvider
    private readonly Func<ICurrentUserProvider> _currentUserProvider;

    public PopulateCreatedUpdatedInterceptor(ITimeProvider timeProvider, Func<ICurrentUserProvider> currentUserProvider)
    {
        _timeProvider = timeProvider;
        _currentUserProvider = currentUserProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PopulateCreatedUpdated(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = new())
    {
        PopulateCreatedUpdated(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void PopulateCreatedUpdated(DbContext dbContext)
    {
        var currentUserProvider = _currentUserProvider();
        var userId = currentUserProvider.UserId ?? throw new InvalidOperationException("Current userId is not set");

        var changedEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(entry => entry.State is EntityState.Added or EntityState.Modified &&
                            entry.Entity is IEntityWithCreatedUpdated)
            .Select(entry => (entry.State, Entity: (IEntityWithCreatedUpdated)entry.Entity)).ToList();

        var currentDateTime = _timeProvider.FixedUtcNow;

        foreach (var (state, entity) in changedEntities)
        {
            if (state == EntityState.Added)
            {
                entity.SetCreated(currentDateTime, userId);
                entity.SetUpdated(currentDateTime, userId);
            }
            if (state == EntityState.Modified)
            {
                entity.SetUpdated(currentDateTime, userId);
            }
        }
    }
}
