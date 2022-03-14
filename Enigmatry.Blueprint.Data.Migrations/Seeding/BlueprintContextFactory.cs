using System.Security.Principal;
using Enigmatry.Blueprint.ApplicationServices.Identity;
using Enigmatry.Blueprint.Infrastructure.Data;
using Enigmatry.BuildingBlocks.EntityFramework.Security;
using Enigmatry.BuildingBlocks.Infrastructure;
using Enigmatry.BuildingBlocks.MediatR;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;

namespace Enigmatry.Blueprint.Data.Migrations.Seeding;

[UsedImplicitly]
public class BlueprintContextFactory : IDesignTimeDbContextFactory<BlueprintContext>
{
    public BlueprintContext CreateDbContext(string[] args) => CreateDbContext(ReadConnectionString(args));

    private BlueprintContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlueprintContext>();
        optionsBuilder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(BlueprintContextFactory).Assembly.FullName));

        var result =
            new BlueprintContext(optionsBuilder.Options, new NullMediator(), new TimeProvider(), new CurrentUserIdProvider(CreateEmptyPrincipal), new NullLogger<BlueprintContext>(), new NullDbContextAccessTokenProvider()) { ModelBuilderConfigurator = DbInitializer.SeedData };
        return result;
    }

    private IPrincipal CreateEmptyPrincipal() => new GenericPrincipal(new GenericIdentity(""), Array.Empty<string>());

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
