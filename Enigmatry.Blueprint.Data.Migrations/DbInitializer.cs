using System.Collections.Generic;
using Enigmatry.Blueprint.Data.Migrations.Seeding;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;

namespace Enigmatry.Blueprint.Data.Migrations
{
    public static class DbInitializer
    {
        private static readonly IList<ISeeding> Seedings = new List<ISeeding>(10);

        static DbInitializer()
        {
            Seedings.Add(new UserSeeding());
        }

        public static void Initialize(BlueprintContext context)
        {
            context.Database.EnsureCreated();

            foreach (ISeeding seeding in Seedings)
            {
                seeding.Seed(context);
            }

            context.SaveChanges();
        }
    }
}