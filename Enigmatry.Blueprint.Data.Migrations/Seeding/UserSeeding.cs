using System;
using Enigmatry.Blueprint.Core;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class UserSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            User user = User.Create(new UserCreateUpdateDto
            {
                Name = "Test",
                UserName = "Test"
            }).CreatedOn(DateTimeOffset.Now, Guid.Empty);

            modelBuilder.Entity<User>().HasData(user.WithId(Guid.NewGuid()));
        }
    }
}