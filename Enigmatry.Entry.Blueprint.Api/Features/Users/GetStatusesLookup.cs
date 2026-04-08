using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Data;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Api.Features.Users;

public static class GetStatusesLookup
{
    [PublicAPI]
    public class Request : LookupRequest<UserStatusId>;

    [UsedImplicitly]
    public class RequestHandler(IRepository<UserStatus, UserStatusId> roleRepository)
        : IRequestHandler<Request, IEnumerable<LookupResponse<UserStatusId>>>
    {
        public async Task<IEnumerable<LookupResponse<UserStatusId>>> Handle(Request request, CancellationToken cancellationToken) =>
            await roleRepository
                .QueryAll()
                .MapToLookup()
                .OrderBy(r => r.Label)
                .ToListAsync(cancellationToken);
    }
}
