using Enigmatry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

public class RoleSeeding : ISeeding
{
    public void Seed(ModelBuilder modelBuilder)
    {
        var role = Role.Create("SystemAdmin", Enumerable.Empty<Permission>().ToList());

        modelBuilder.Entity<Role>().HasData(role.WithId(Role.SystemAdminRoleId));
    }
}
