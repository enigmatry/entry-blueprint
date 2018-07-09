using System;
using Enigmatry.Blueprint.Model.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    public class UserSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(User.Create("Test", "Test", DateTimeOffset.Now).WithId(1));
        }
    }
}