using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    public partial class SeedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ResourceSql("SeedTestData.sql");
        }
    }
}
