using System.Collections.Generic;
using Enigmatry.Blueprint.Data.Migrations.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations
{
    public static class DbInitializer
    {
        private static readonly IList<ISeeding> Seedings = new List<ISeeding>(10);

        static DbInitializer()
        {
            Seedings.Add(new UserSeeding());
        }

        // EF Core way of seeding data: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (ISeeding seeding in Seedings)
            {
                seeding.Seed(modelBuilder);
            }
        }
    }
}