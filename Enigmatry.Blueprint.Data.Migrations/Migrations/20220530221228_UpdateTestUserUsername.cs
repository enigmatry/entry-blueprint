using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class UpdateTestUserUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"),
                column: "UserName",
                value: "test@enigmatry.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"),
                column: "UserName",
                value: "Test");
        }
    }
}
