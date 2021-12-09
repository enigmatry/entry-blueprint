using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class ProductDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("65fb31fa-ff40-4bd5-8fe6-755122b6428b"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("67a75734-d04a-4661-95d9-24879122c901"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("767c974c-8c07-43f5-8c6f-0cc1eef3c65a"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"),
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"),
                column: "Description",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");
        }
    }
}
