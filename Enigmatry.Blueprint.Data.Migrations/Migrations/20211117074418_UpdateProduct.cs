using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "Product",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                columns: new[] { "Discount", "HasDiscount" },
                values: new object[] { 25f, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"),
                columns: new[] { "Discount", "HasDiscount" },
                values: new object[] { 10f, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "Product");
        }
    }
}
