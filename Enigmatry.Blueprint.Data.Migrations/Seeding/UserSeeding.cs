using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

public class UserSeeding : ISeeding
{
    public void Seed(ModelBuilder modelBuilder)
    {
        var user = User.Create(new CreateOrUpdateUser.Command
        {
            Name = "Test",
            UserName = "Test"
        });

        user.SetCreated(new DateTimeOffset(2019, 5, 6, 14, 31, 0, TimeSpan.FromHours(0)));
        modelBuilder.Entity<User>().HasData(user.WithId(User.TestUserId));
    }
}
