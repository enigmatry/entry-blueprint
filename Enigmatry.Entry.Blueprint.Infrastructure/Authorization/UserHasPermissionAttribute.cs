using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization.Attributes;

namespace Enigmatry.Entry.Blueprint.Infrastructure.Authorization;

public sealed class UserHasPermissionAttribute : UserHasPermissionAttribute<PermissionId>
{
    public UserHasPermissionAttribute(params PermissionId[] permissions) : base(permissions)
    {
    }
}
