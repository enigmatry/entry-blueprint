using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Tests;

public static class DbContextExtensions
{
    public static IQueryable<T> QueryDbSkipCache<T>(this DbContext context) where T : class => context.Set<T>().AsNoTracking();

    public static async Task DeleteByIdsAndSaveChangesAsync<T, TId>(this DbContext context, params TId[] ids) where T : class
    {
        foreach (var id in ids)
        {
            await context.DeleteByIdAsync<T, TId>(id);
        }

        await context.SaveChangesAsync();
    }

    public static async Task DeleteByIdAsync<T, TId>(this DbContext context, TId id) where T : class
    {
        var dbSet = context.Set<T>();
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
        }
    }
}
