using System;
using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Data.Migrations.Seeding;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Data.Migrations.Runner
{
    [UsedImplicitly]
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Connection string should be provided as the parameter");
                return;
            }

            try
            {
                var factory = new BlueprintContextFactory();
                using var context = factory.CreateDbContext(args[0]);
                Console.WriteLine("Updating database...");

                IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations().ToList();

                Console.WriteLine(pendingMigrations.Any()
                    ? $"Executing {pendingMigrations.Count()} pending migrations: {String.Join(Environment.NewLine, pendingMigrations)}"
                    : "No pending migrations");

                context.Database.Migrate();

                // seeding is configured in BlueprintContextFactory

                Console.WriteLine("Database is successfully updated");
#if DEBUG
                Console.ReadKey();

#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
#if DEBUG
                Console.ReadKey();
#endif
                Environment.Exit(-1);
            }
        }
    }
}
