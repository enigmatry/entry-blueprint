using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddCCName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "User");
        }
    }
}
