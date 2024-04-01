using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    public partial class SeedSystemUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "EmailAddress", "FullName", "RoleId", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"), new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"), new DateTimeOffset(new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "N/A", "System User", new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0"), new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"), new DateTimeOffset(new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
