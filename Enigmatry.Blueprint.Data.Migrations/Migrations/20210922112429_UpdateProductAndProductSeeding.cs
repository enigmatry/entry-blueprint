using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class UpdateProductAndProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"),
                column: "Amount",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"),
                column: "Amount",
                value: 89);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                column: "Amount",
                value: 23);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"),
                column: "Amount",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"),
                column: "Amount",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"),
                column: "Amount",
                value: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Product");
        }
    }
}
