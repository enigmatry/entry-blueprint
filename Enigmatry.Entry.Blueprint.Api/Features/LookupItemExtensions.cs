using Enigmatry.Entry.Blueprint.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Api.Features;

public static class LookupItemExtensions
{
    public static IQueryable<LookupResponse<TId>> MapToLookup<TId>(this IQueryable<ILookupItem<TId>> query) =>
        query.Select(x => new LookupResponse<TId>
        {
            Value = x.Id,
            Label = x.Name
        });
}
