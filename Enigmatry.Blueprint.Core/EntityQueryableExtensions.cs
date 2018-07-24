using System;
using System.Linq;

namespace Enigmatry.Blueprint.Core
{
    public static class EntityQueryableExtensions
    {
        public static IQueryable<T> ById<T>(this IQueryable<T> query, Guid id) where T : Entity
        {
            return query.Where(e => e.Id == id);
        }

        public static IQueryable<T> ExceptWithId<T>(this IQueryable<T> query, Guid? id) where T : Entity
        {
            if (!id.HasValue)
            {
                return query;
            }

            return query.Where(e => e.Id != id);
        }
    }
}