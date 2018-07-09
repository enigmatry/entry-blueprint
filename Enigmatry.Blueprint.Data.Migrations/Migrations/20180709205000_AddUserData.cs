using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class AddUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTimeOffset(new DateTime(2018, 7, 9, 22, 49, 59, 868, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTimeOffset(new DateTime(2018, 7, 9, 22, 35, 1, 996, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));
        }
    }
}
