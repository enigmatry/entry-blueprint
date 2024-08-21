using Enigmatry.Entry.Blueprint.Data.Migrations.Seeding;
using Enigmatry.Entry.Blueprint.Domain.Authorization;
using Enigmatry.Entry.Blueprint.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Entry.Blueprint.Data.Migrations;

internal class UserSeeding : ISeeding
{
    public void Seed(ModelBuilder modelBuilder)
    {
        var asOf = new DateTimeOffset(2024, 3, 29, 0, 0, 0, TimeSpan.Zero);
        var user = new
        {
            Id = User.SystemUserId,
            EmailAddress = "N/A",
            FullName = "System User",
            RoleId = Role.SystemAdminRoleId,
            CreatedById = User.SystemUserId,
            CreatedOn = asOf,
            UpdatedById = User.SystemUserId,
            UpdatedOn = asOf,
            UserStatusId = UserStatusId.Active
        };
        modelBuilder.Entity<User>().HasData(user);
    }
}
