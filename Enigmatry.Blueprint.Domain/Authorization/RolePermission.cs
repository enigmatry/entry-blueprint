#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
namespace Enigmatry.Blueprint.Domain.Authorization;

public record RolePermission
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}
