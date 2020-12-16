using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Api
{
    public static class QueryableMappingExtensions
    {
        public static async Task<TDestination> SingleOrDefaultMappedAsync<TSource, TDestination>(this IQueryable<TSource> query, IMapper mapper, CancellationToken cancellationToken = default)
        {
            var item = await query.SingleOrDefaultAsync(cancellationToken);
            return mapper.Map<TDestination>(item);
        }

        public static async Task<List<TDestination>> ToListMappedAsync<TSource, TDestination>(this IQueryable<TSource> query, IMapper mapper, CancellationToken cancellationToken = default)
        {
            var items = await query.ToListAsync(cancellationToken);
            return mapper.Map<List<TDestination>>(items);
        }
    }
}
