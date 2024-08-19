using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"),
                column: "StatusId",
                value: 1);

            migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Active Status Description", "Active" },
                    { 2, "Inactive Status Description", "Inactive" }
                });
            
            migrationBuilder.Sql("Update [User] set StatusId = 1");

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusId",
                table: "User",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserStatus_StatusId",
                table: "User",
                column: "StatusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserStatus_StatusId",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserStatus");

            migrationBuilder.DropIndex(
                name: "IX_User_StatusId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "User");
        }
    }
}
