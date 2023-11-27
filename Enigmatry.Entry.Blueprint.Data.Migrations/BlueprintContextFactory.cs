using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Enigmatry.Entry.Blueprint.Data.Migrations;

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
            new BlueprintContext(optionsBuilder.Options)
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
