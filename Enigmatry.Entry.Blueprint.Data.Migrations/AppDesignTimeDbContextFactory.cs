using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Enigmatry.Entry.Blueprint.Data.Migrations;

[UsedImplicitly]
public class AppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args) => CreateDbContext(ReadConnectionString(args));

    private static AppDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString,
            b =>
            {
                b.MigrationsAssembly(typeof(AppDesignTimeDbContextFactory).Assembly.FullName);
            });

        var result =
            new AppDbContext(optionsBuilder.Options)
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
