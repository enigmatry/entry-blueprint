using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enigmatry.Blueprint.Core.Data
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> QueryAll();
        IQueryable<T> QueryAllIncluding(params Expression<Func<T, object>>[] paths);
        void Add(T item);
        void Delete(T item);
        void Delete(int id);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
        T FindByIdNoCache(int id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
