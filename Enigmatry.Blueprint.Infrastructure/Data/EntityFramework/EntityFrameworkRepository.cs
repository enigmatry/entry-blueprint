using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        public EntityFrameworkRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        protected DbSet<T> DbSet { get; }

        protected DbContext DbContext { get; }

        public IQueryable<T> QueryAll() => DbSet;

        public IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths)
        {
            return paths == null
                ? throw new ArgumentNullException(nameof(paths))
                : paths.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Id != Guid.Empty)
            {
                throw new InvalidOperationException("You cannot add entity that has Id set.");
            }

            DbSet.Add(item);
        }

        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Remove(item);
        }

        public void Delete(Guid id)
        {
            T item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public T FindById(Guid id) => DbSet.Find(id);

        public async Task<T> FindByIdAsync(Guid id) => await DbSet.FindAsync(id);

        public T FindByIdNoCache(Guid id) => DbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);

        public void DeleteRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities);
    }
}
