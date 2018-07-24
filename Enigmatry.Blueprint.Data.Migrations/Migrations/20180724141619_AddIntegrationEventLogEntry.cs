using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class AddIntegrationEventLogEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4216c3bc-7ab3-4707-9cfb-ef4b3c5617f4"));

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    EventTypeName = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TimesSent = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOn", "Name", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("0071e8cd-c3bc-4dbd-abaf-6946a2d064f1"), new DateTimeOffset(new DateTime(2018, 7, 24, 16, 16, 18, 753, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Test", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0071e8cd-c3bc-4dbd-abaf-6946a2d064f1"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOn", "Name", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("4216c3bc-7ab3-4707-9cfb-ef4b3c5617f4"), new DateTimeOffset(new DateTime(2018, 7, 24, 9, 50, 19, 809, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Test", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test" });
        }
    }
}
