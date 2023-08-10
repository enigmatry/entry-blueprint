using Enigmatry.Blueprint.Domain.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

public class RoleSeeding : ISeeding
{
    private readonly IList<Role> _roles = new List<Role>
    {
        new()
        {
            Id = Role.SystemAdminRoleId,
            Name = "SystemAdmin"
        }
    };

    public void Seed(ModelBuilder modelBuilder) => modelBuilder.Entity<Role>().HasData(_roles);
}
