using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOn", "Name", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("4216c3bc-7ab3-4707-9cfb-ef4b3c5617f4"), new DateTimeOffset(new DateTime(2018, 7, 24, 9, 50, 19, 809, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Test", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test" });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
