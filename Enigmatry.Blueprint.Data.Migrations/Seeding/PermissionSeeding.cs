using Enigmatry.Blueprint.Domain.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class PermissionSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var allPermissions = Enum.GetValues<PermissionId>()
                .Select(permissionId => new Permission(permissionId));

            modelBuilder.Entity<Permission>().HasData(allPermissions);
        }
    }
}
