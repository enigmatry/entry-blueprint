using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

public static class GetRoleLookup
{
    [PublicAPI]
    public class Request : LookupRequest<Guid>;

    [UsedImplicitly]
    public class RequestHandler(IRepository<Role> roleRepository)
        : IRequestHandler<Request, IEnumerable<LookupResponse<Guid>>>
    {
        public async Task<IEnumerable<LookupResponse<Guid>>> Handle(Request request, CancellationToken cancellationToken) =>
            await roleRepository
                .QueryAll()
                .MapToRoleLookup()
                .OrderBy(r => r.Label)
                .ToListAsync(cancellationToken);
    }

    public static IQueryable<LookupResponse<Guid>> MapToRoleLookup(this IQueryable<Role> query) =>
        query.Select(x => new LookupResponse<Guid>
        {
            Value = x.Id,
            Label = x.Name
        });
}
