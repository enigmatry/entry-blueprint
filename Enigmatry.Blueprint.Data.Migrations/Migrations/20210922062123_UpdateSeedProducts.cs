using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class UpdateSeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoLink",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"),
                column: "InfoLink",
                value: "https://en.wikipedia.org/wiki/Lada_Niva");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                column: "InfoLink",
                value: "https://en.wikipedia.org/wiki/Dune_(novel)");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"),
                column: "InfoLink",
                value: "https://www.salto.rs/#belgrade-ipa");

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Code", "ContactEmail", "ContactPhone", "CreatedById", "CreatedOn", "ExpiresOn", "InfoLink", "Name", "Price", "Type", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"), "VWVW12345678", "vw_camper@vw.com", "+381 32 332 7689", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "https://en.wikipedia.org/wiki/Volkswagen_Type_2", "Volkswagen Type 2", 8799.5, 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"), "FOOD12345678", "burek@burek.com", "+381 11 113 6651", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "", "Burek", 2.5, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"), "ZXAB14444678", "sardines@ocean.com", "+381 11 451 8709", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2050, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://www.youtube.com/watch?v=WPpFjl8qeM4&ab_channel=DiscoveryUK", "Sardines", 7.3300000000000001, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"));

            migrationBuilder.DropColumn(
                name: "InfoLink",
                table: "Product");
        }
    }
}
