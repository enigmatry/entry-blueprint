using Enigmatry.Entry.Core.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Entry.Blueprint.Data.Migrations;

public static class MigrationBuilderExtensions
{
    public static void ResourceSql(this MigrationBuilder migrationBuilder, string scriptName)
    {
        var sql =  EmbeddedResource.ReadResourceContent($"Enigmatry.Entry.Blueprint.Data.Migrations.Scripts.{scriptName}", typeof(DbInitializer).Assembly);
        if (!sql.HasContent())
        {
            throw new InvalidOperationException($"Script could not be loaded: {scriptName}");
        }
        migrationBuilder.Sql(sql);
    }
}
