using Enigmatry.Entry.Core.Entities;
using JetBrains.Annotations;
#pragma warning disable CA1711

namespace Enigmatry.Blueprint.Domain.Authorization;

public class Permission : EntityWithTypedId<PermissionId>
{
    public const int NameMaxLength = 100;

    private readonly IList<Role> _roles = new List<Role>();

    public string Name { get; private init; } = String.Empty;
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    [UsedImplicitly]
    private Permission() { }

    public Permission(PermissionId permissionId)
    {
        Id = permissionId;
        Name = permissionId.ToString();
    }

    public static IEnumerable<Permission> CreateAll() =>
        Enum.GetValues<PermissionId>()
            .Except(new[] { PermissionId.None })
            .Select(permissionId => new Permission(permissionId));
}
