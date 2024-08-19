using Enigmatry.Entry.Blueprint.Domain.Authorization;

namespace Enigmatry.Entry.Blueprint.Domain.Identity;

public sealed class PermissionsContext(IReadOnlyCollection<PermissionId> values)
{
    private IReadOnlyCollection<PermissionId> Values { get; } = values;
    public static PermissionsContext Empty { get; } = new([]);

    public bool Contains(PermissionId permissionId) => Values.Contains(permissionId);

    public bool ContainsAny(IEnumerable<PermissionId> permissionIds) => Values.Intersect(permissionIds).Any();
}
