using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Database;

internal class TestDatabase
{
    public string ConnectionString { get; }
    private static MsSqlContainer? _container;

    public TestDatabase()
    {
        // To use a local sqlServer instance, Create an Environment variable using R# Test Runner, with name "IntegrationTestsConnectionString"
        // and value: "Server=.;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
        var connectionString = Environment.GetEnvironmentVariable("IntegrationTestsConnectionString");

        if (!String.IsNullOrEmpty(connectionString))
        {
            // do not write ConnectionString to the console since it might contain username/password 
            ConnectionString = connectionString;
        }
        else
        {
            _container ??= new MsSqlBuilder()
                .WithAutoRemove(true)
                .WithCleanUp(true)
                .Build();

            _container!.StartAsync().Wait();
            ConnectionString = _container.GetConnectionString();
            WriteLine($"Docker SQL connection string: {ConnectionString}");
        }
    }

    public static async Task ResetAsync(DbContext dbContext) => await DatabaseInitializer.RecreateDatabaseAsync(dbContext, new[] { "__EFMigrationsHistory", "Role", "RolePermission", "Permission" });

    private static void WriteLine(string value) => TestContext.WriteLine(value);
}
