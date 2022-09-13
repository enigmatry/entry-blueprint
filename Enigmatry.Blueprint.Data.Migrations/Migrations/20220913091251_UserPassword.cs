using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class UserPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"),
                column: "Password",
                value: "Test1234!");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");
        }
    }
}
