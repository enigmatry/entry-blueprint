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
                .MapToLookup()
                .OrderBy(r => r.Label)
                .ToListAsync(cancellationToken);
    }
}
