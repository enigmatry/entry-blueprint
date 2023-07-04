#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
using Enigmatry.Entry.Core.Entities;

namespace Enigmatry.Blueprint.Domain.Authorization;

public class Permission : EntityWithGuidId
{
    public const int NameMaxLength = 100;

    public string Name { get; private set; } = String.Empty;

    public ICollection<Role>? Roles { get; private set; }

    public static Permission Create(string name)
    {
        var result = new Permission
        {
            Name = name
        };

        return result;
    }
}
