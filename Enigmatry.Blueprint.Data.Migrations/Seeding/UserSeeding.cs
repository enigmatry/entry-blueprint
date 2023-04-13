using Enigmatry.Blueprint.Domain.Identity;
using Enigmatry.Blueprint.Domain.Identity.Commands;
using Enigmatry.Entry.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

public class UserSeeding : ISeeding
{
    public void Seed(ModelBuilder modelBuilder)
    {
        var systemUser = User.Create(new CreateOrUpdateUser.Command
        {
            Name = "System",
            UserName = "system@mail.com"
        })
        .WithId(User.SystemUserId);
        systemUser.SetCreated(new DateTimeOffset(2019, 5, 6, 14, 31, 0, TimeSpan.FromHours(0)));
        var users = GenerateUsers(200);
        users.Add(systemUser);

        modelBuilder.Entity<User>().HasData(users);
    }

    private static List<User> GenerateUsers(int numberOfUsers) =>
        Enumerable.Range(0, numberOfUsers)
            .Select(index => User.Create(new CreateOrUpdateUser.Command
            {
                Name = $"Test_{index}",
                UserName = $"test.{index}@.mail.com"
            }))
            .ToList();
}
