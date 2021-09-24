using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                columns: new[] { "ContactPhone", "Name" },
                values: new object[] { "+253 56 334 4889", "Dune I" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Code", "ContactEmail", "ContactPhone", "CreatedById", "CreatedOn", "ExpiresOn", "Name", "Price", "Type", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("04c45383-1372-4312-bb38-5edfa569db66"), "XXXX12345678", "info@lada.com", "+381 21 661 6432", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Lada Niva", 15335.0, 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Code", "ContactEmail", "ContactPhone", "CreatedById", "CreatedOn", "ExpiresOn", "Name", "Price", "Type", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"), "ABCD12345678", "info@salto.rs", "+381 60 399 8871", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Salto IPA", 2.6000000000000001, 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                columns: new[] { "ContactPhone", "Name" },
                values: new object[] { "+1253 3344-889", "Dune" });
        }
    }
}
