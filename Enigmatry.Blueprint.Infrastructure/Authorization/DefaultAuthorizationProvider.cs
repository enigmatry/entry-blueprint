using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Entry.AspNetCore.Authorization;

namespace Enigmatry.Blueprint.Infrastructure.Authorization;

public class DefaultAuthorizationProvider : IAuthorizationProvider<PermissionId>
{
    private readonly ICurrentUserProvider _currentUserProvider;

    public DefaultAuthorizationProvider(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }

    public bool AuthorizePermissions(IEnumerable<PermissionId> permissions)
        => _currentUserProvider.User?.HasAnyPermission(permissions) ?? false;
}
