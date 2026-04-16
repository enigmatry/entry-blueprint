using Enigmatry.Entry.Blueprint.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Api.Features;

public static class LookupItemExtensions
{
    extension<TId>(IQueryable<ILookupItem<TId>> query)
    {
        public IQueryable<LookupResponse<TId>> MapToLookup() =>
            query.Select(x => new LookupResponse<TId>
            {
                Value = x.Id,
                Label = x.Name
            });
    }
}
