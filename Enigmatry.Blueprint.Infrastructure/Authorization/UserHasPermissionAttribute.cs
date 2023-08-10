using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization.Attributes;

namespace Enigmatry.Blueprint.Infrastructure.Authorization;

public sealed class UserHasPermissionAttribute : UserHasPermissionAttribute<PermissionId>
{
    public UserHasPermissionAttribute(params PermissionId[] permissions) : base(permissions)
    {
    }
}
