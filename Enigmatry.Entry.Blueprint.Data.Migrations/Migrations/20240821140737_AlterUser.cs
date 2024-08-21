using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AlterUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserStatus_StatusId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "User",
                newName: "UserStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_User_StatusId",
                table: "User",
                newName: "IX_User_UserStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserStatus_UserStatusId",
                table: "User",
                column: "UserStatusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserStatus_UserStatusId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserStatusId",
                table: "User",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserStatusId",
                table: "User",
                newName: "IX_User_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserStatus_StatusId",
                table: "User",
                column: "StatusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
