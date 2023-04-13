using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class AddDiscountExpiresOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DiscountExpiresOn",
                table: "Product",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountExpiresOn",
                table: "Product");
        }
    }
}
