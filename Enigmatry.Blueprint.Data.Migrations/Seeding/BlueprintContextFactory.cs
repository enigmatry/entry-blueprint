using System;
using Enigmatry.Blueprint.Infrastructure;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Infrastructure.MediatR;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    [UsedImplicitly]
    public class BlueprintContextFactory : IDesignTimeDbContextFactory<BlueprintContext>
    {
        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        public BlueprintContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlueprintContext>();
            optionsBuilder.UseSqlServer(ReadConnectionString(),
                b => b.MigrationsAssembly("Enigmatry.Blueprint.Data.Migrations"));

            var result =
                new BlueprintContext(optionsBuilder.Options, new NoMediator(), new TimeProvider())
                {
                    ModelBuilderConfigurator = DbInitializer.SeedData
                };
            return result;
        }

        private static string ReadConnectionString()
        {
            var environmentVariableName = "MigrateDatabaseConnectionString";
            string connectionString = Environment.GetEnvironmentVariable(environmentVariableName);
            Console.WriteLine(string.IsNullOrEmpty(connectionString)
                ? $"{environmentVariableName} environment variable is not set."
                : $"{environmentVariableName} variable was found. " + connectionString);

            return connectionString;
        }
    }
}