using Enigmatry.Entry.AspNetCore.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Identity;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Authorization;

public class DefaultAuthorizationProvider(ICurrentUserProvider currentUserProvider) : IAuthorizationProvider<PermissionId>
{
    public bool AuthorizePermissions(IEnumerable<PermissionId> permissions)
        => currentUserProvider.User?.Permissions.ContainsAny(permissions) ?? false;
}
