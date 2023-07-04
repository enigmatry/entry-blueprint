using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Entry.AspNetCore.Authorization;

namespace Enigmatry.Blueprint.ApplicationServices.Authorization;

public class DefaultAuthorizationProvider : IAuthorizationProvider
{
    private readonly ICurrentUserProvider _currentUserProvider;

    public DefaultAuthorizationProvider(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }

    public bool HasAnyRole(IEnumerable<string> roles) => roles.Contains(_currentUserProvider.User!.Role!.Name);

    public bool HasAnyPermission(IEnumerable<string> permission) =>
        (_currentUserProvider.User!
            .Role
            .Permissions ?? Enumerable.Empty<Permission>().ToList())
            .Select(p => p.Name)
            .Intersect(permission)
            .Any();
}
