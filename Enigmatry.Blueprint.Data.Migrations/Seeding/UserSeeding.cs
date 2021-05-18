using System;
using Enigmatry.BuildingBlocks.Core.Entities;
using Enigmatry.Blueprint.Model.Identity;
using Enigmatry.Blueprint.Model.Identity.Commands;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class UserSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var user = User.Create(new UserCreateOrUpdate.Command
            {
                Name = "Test",
                UserName = "Test"
            });

            user.SetCreated(new DateTimeOffset(2019, 5, 6, 14, 31, 0, TimeSpan.FromHours(0)));
            modelBuilder.Entity<User>().HasData(user.WithId(new Guid("8207DB25-94D1-4F3D-BF18-90DA283221F7")));
        }
    }
}
