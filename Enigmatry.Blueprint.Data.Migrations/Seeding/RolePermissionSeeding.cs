using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Blueprint.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class RolePermissionSeeding : ISeeding
    {
        private readonly IEnumerable<(Guid RoleId, PermissionId[] Permissions)> _rolesPermissions = new List<(Guid RoleId, PermissionId[] Permissions)>()
        {
            new (Role.SystemAdminRoleId, Enum.GetValues<PermissionId>()) // all permissions
        };

        public void Seed(ModelBuilder modelBuilder)
        {
            IList<RolePermission> rolePermissions = _rolesPermissions
                .SelectMany(tuple => tuple.Permissions
                    .Select(permissionId => new RolePermission { RoleId = tuple.RoleId, PermissionId = permissionId }))
                .ToList();

            modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
        }
    }
}
