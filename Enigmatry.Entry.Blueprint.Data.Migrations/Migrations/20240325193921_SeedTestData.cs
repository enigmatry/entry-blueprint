using Enigmatry.Entry.Core.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = EmbeddedResource.ReadResourceContent("Enigmatry.Entry.Blueprint.Data.Migrations.Scripts.SeedTestData.sql",
                typeof(AddTestUser).Assembly);
            migrationBuilder.Sql(sql);
        }
    }
}
