using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Seeding
{
    public class RolePermissionSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(Permission.CreateAll());

            modelBuilder.Entity<Role>().HasData(Role.CreateAll());

            var rolePermissions = GetRolePermissions();
            modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
        }

        private static IList<RolePermission> GetRolePermissions() =>
            Role.GetAllRolePermissions()
                .SelectMany(tuple => tuple.Permissions
                    .Select(permissionId => new RolePermission { RoleId = tuple.RoleId, PermissionId = permissionId }))
                .ToList();
    }
}
