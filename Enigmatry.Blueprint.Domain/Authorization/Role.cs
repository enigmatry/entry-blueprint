using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Authorization;

public class Role : EntityWithGuidId
{
    public const int NameMaxLength = 100;
    public static readonly Guid SystemAdminRoleId = new("028e686d-51de-4dd9-91e9-dfb5ddde97d0");

    public string Name { get; private set; } = String.Empty;

    public ICollection<User>? Users { get; private set; }
    public ICollection<Permission>? Permissions { get; private set; }

    public static Role Create(string name, ICollection<Permission>? permissions)
        => new()
        {
            Name = name,
            Permissions = permissions
        };
}
