using Enigmatry.Entry.AspNetCore.Tests.Utilities.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Respawn;
using Respawn.Graph;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Database;

internal static class DatabaseInitializer
{
    public static async Task RecreateDatabaseAsync(DbContext dbContext, IEnumerable<string> tablesToIgnore)
    {
        if (HasSchemaChanges(dbContext))
        {
            RecreateDatabase(dbContext);
        }
        else
        {
            await ResetDataAsync(dbContext, tablesToIgnore);
        }
    }

    private static bool HasSchemaChanges(DbContext dbContext)
    {
        try
        {
            var dbDoesNotExist = !dbContext.Database.CanConnect(); // this will throw SqlException if connection to server can not be made, and true / false depending if db exists
            return dbDoesNotExist || dbContext.Database.GetPendingMigrations().Any();
        }
        catch (SqlException ex)
        {
            WriteLine("Error connecting to SqlServer:");
            WriteLine(ex.ToString());
            throw;
        }
    }

    private static void RecreateDatabase(DbContext dbContext)
    {
        DropAllDbObjects(dbContext.Database);
        dbContext.Database.Migrate();
    }

    private static async Task ResetDataAsync(DbContext dbContext, IEnumerable<string> tablesToIgnore)
    {
        var connectionString = dbContext.Database.GetConnectionString() ?? String.Empty;

        var respawner = await Respawner.CreateAsync(connectionString,
            new RespawnerOptions
            {
                TablesToIgnore = tablesToIgnore.Select(name => new Table(name)).ToArray()
            });

        await respawner.ResetAsync(connectionString);
    }

    private static void DropAllDbObjects(DatabaseFacade database)
    {
        try
        {
            var dropAllSql = DatabaseHelpers.DropAllSql;
            foreach (var statement in dropAllSql.SplitStatements())
            {
                database.ExecuteSqlRaw(statement);
            }
        }
        catch (SqlException ex)
        {
            const int cannotOpenDatabaseErrorNumber = 4060;
            if (ex.Number == cannotOpenDatabaseErrorNumber)
            {
                WriteLine("Error while trying to drop all objects from database. Maybe database does not exist.");
                WriteLine("Continuing...");
                WriteLine(ex.ToString());
            }
            else
            {
                throw;
            }
        }
    }

    private static void WriteLine(string message) => TestContext.WriteLine(message);
}
