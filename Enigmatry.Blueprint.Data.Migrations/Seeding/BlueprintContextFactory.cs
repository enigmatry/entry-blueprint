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
        private const string DevelopmentConnectionString = "Server=.;Database=Blueprint-Core;Trusted_Connection=True;MultipleActiveResultSets=true";

        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        public BlueprintContext CreateDbContext(string[] args)
        {
            return CreateDbContext(ReadConnectionString());
        }

        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        private static string ReadConnectionString()
        {
            const string environmentVariableName = "MigrateDatabaseConnectionString";
            string? connectionString = Environment.GetEnvironmentVariable(environmentVariableName);
            Console.WriteLine(String.IsNullOrEmpty(connectionString)
                ? $"{environmentVariableName} environment variable is not set."
                : $"{environmentVariableName} variable was found. ");

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine($"Falling back to developers connection string: '{DevelopmentConnectionString}'");
                return DevelopmentConnectionString;
            }

            return connectionString;
        }

        public BlueprintContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlueprintContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Enigmatry.Blueprint.Data.Migrations"));

            var result =
                new BlueprintContext(optionsBuilder.Options, new NoMediator(), new TimeProvider())
                {
                    ModelBuilderConfigurator = DbInitializer.SeedData
                };
            return result;
        }
    }
}
