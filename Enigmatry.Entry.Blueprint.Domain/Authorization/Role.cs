using Enigmatry.Entry.Blueprint.Domain.Users;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Entry.Blueprint.Domain.Authorization;

public class Role : EntityWithGuidId
{
    public const int NameMaxLength = 100;

    public static readonly Guid SystemAdminRoleId = new("028e686d-51de-4dd9-91e9-dfb5ddde97d0");

    public string Name { get; init; } = String.Empty;

    private readonly IList<User> _users = [];
    private IList<Permission> _permissions = [];

    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    public void SetPermissions(IEnumerable<Permission> permissions) => _permissions = permissions.ToList();

    public static IEnumerable<Role> CreateAll()
    {
        yield return new Role { Id = SystemAdminRoleId, Name = "SystemAdmin" };
    }

    public static IEnumerable<(Guid RoleId, PermissionId[] Permissions)> GetAllRolePermissions()
    {
        yield return new ValueTuple<Guid, PermissionId[]>(SystemAdminRoleId, Enum.GetValues<PermissionId>().Except([PermissionId.None]).ToArray()); // all permissions
    }
}
