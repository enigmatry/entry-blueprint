using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Enigmatry.Blueprint.ApplicationServices.Identity;
using Enigmatry.Blueprint.Infrastructure;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Enigmatry.Blueprint.Infrastructure.MediatR;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding
{
    [UsedImplicitly]
    public class BlueprintContextFactory : IDesignTimeDbContextFactory<BlueprintContext>
    {
        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        public BlueprintContext CreateDbContext(string[] args) => CreateDbContext(ReadConnectionString());

        public BlueprintContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlueprintContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Enigmatry.Blueprint.Data.Migrations"));

            var result =
                new BlueprintContext(optionsBuilder.Options, new NoMediator(), new TimeProvider(), new CurrentUserIdProvider(CreateEmptyPrincipal), new NullLogger<BlueprintContext>(), new NullDbContextAccessTokenProvider()) {ModelBuilderConfigurator = DbInitializer.SeedData};
            return result;
        }

        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        private static string ReadConnectionString()
        {
            const string environmentVariableName = "MigrateDatabaseConnectionString";
            var connectionString = Environment.GetEnvironmentVariable(environmentVariableName);
            Console.WriteLine(String.IsNullOrEmpty(connectionString)
                ? $"{environmentVariableName} environment variable is not set."
                : $"{environmentVariableName} variable was found. ");

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine($"Falling back to developers connection string: '{DevelopmentConnectionsStrings.MainConnectionString}'");
                return DevelopmentConnectionsStrings.MainConnectionString;
            }

            return connectionString;
        }

        private IPrincipal CreateEmptyPrincipal() => new GenericPrincipal(new GenericIdentity(""), Array.Empty<string>());

        private class NullDbContextAccessTokenProvider : IDbContextAccessTokenProvider
        {
            public Task<string> GetAccessTokenAsync() => Task.FromResult(String.Empty);
        }
    }
}
