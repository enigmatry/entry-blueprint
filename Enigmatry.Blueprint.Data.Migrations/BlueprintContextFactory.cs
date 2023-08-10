using Enigmatry.Blueprint.Infrastructure.Data;
using Enigmatry.Blueprint.Infrastructure.Identity;
using Enigmatry.Entry.EntityFramework.Security;
using Enigmatry.Entry.Infrastructure;
using Enigmatry.Entry.MediatR;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enigmatry.Blueprint.Data.Migrations;

[UsedImplicitly]
public class BlueprintContextFactory : IDesignTimeDbContextFactory<BlueprintContext>
{
    public BlueprintContext CreateDbContext(string[] args) => CreateDbContext(ReadConnectionString(args));

    private static BlueprintContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlueprintContext>();
        optionsBuilder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(BlueprintContextFactory).Assembly.FullName));

        var result =
            new BlueprintContext(optionsBuilder.Options,
                new NullMediator(), new TimeProvider(), () => new NullCurrentUserProvider(),
                new NullDbContextAccessTokenProvider(), new NullLogger<BlueprintContext>())
            {
                ModelBuilderConfigurator = DbInitializer.SeedData
            };
        return result;
    }

    private static string ReadConnectionString(string[] args)
    {
        var connectionString = args.FirstOrDefault();

        if (String.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine($"Connection string is not provided in the arguments. Falling back to developers connection string: '{DevelopmentConnectionsStrings.MainConnectionString}'");
            connectionString = DevelopmentConnectionsStrings.MainConnectionString;
        }
        return connectionString;
    }
}
