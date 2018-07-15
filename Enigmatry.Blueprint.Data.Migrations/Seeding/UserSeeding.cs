using System;
using Enigmatry.Blueprint.Model;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class UserSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            User user = User.Create(new UserCreateDto
            {
                Name = "Test",
                UserName = "Test",
                CreatedOn = DateTimeOffset.Now
            });

            modelBuilder.Entity<User>().HasData(user.WithId(1));
        }
    }
}