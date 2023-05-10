using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    InfoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FreeShipping = table.Column<bool>(type: "bit", nullable: false),
                    HasDiscount = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Name", "UpdatedById", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), null, new DateTimeOffset(new DateTime(2019, 5, 6, 14, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Amount", "Code", "ContactEmail", "ContactPhone", "CreatedById", "CreatedOn", "Description", "Discount", "ExpiresOn", "FreeShipping", "HasDiscount", "InfoLink", "Name", "Price", "Type", "UpdatedById", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("04c45383-1372-4312-bb38-5edfa569db66"), 1, "XXXX12345678", "info@lada.com", "+381 (021) 661 6432", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, null, true, false, "https://en.wikipedia.org/wiki/Lada_Niva", "Lada Niva", 15335.0, 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"), 89, "FOOD12345678", "burek@burek.com", "+381 (011) 113 6651", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, null, false, false, "", "Burek", 2.5, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("65fb31fa-ff40-4bd5-8fe6-755122b6428b"), 22, "TKMB33774422", "harper.lee@book.com", "+253 (056) 331-1178", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, null, false, false, "https://en.wikipedia.org/wiki/To_Kill_a_Mockingbird", "To Kill a Mockingbird", 9.0899999999999999, 2, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("67a75734-d04a-4661-95d9-24879122c901"), 54, "KAPS11223344", "kapsalon@nl.fast.food.com", "+31 (098) 221 3489", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, null, false, false, "https://en.wikipedia.org/wiki/Kapsalon", "Kapsalon", 5.5, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("767c974c-8c07-43f5-8c6f-0cc1eef3c65a"), 68, "KIBL12344321", "kibbeling@nl.fast.food.com", "+31 (098) 777 3379", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, null, false, false, "https://en.wikipedia.org/wiki/Kibbeling", "Kibbeling", 4.5, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"), 23, "XYZW12345678", "frank.herbert@gmail.com", "+253 (056) 334 4889", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", 25f, null, true, true, "https://en.wikipedia.org/wiki/Dune_(novel)", "Dune I", 13.699999999999999, 2, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"), 13, "ZXAB14444678", "sardines@ocean.com", "+381 (011) 451-8709", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, new DateTimeOffset(new DateTime(2050, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "https://www.youtube.com/watch?v=WPpFjl8qeM4&ab_channel=DiscoveryUK", "Sardines", 7.3300000000000001, 0, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"), 3, "VWVW12345678", "vw_camper@vw.com", "+381 (032) 332 7689", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", 10f, null, true, true, "https://en.wikipedia.org/wiki/Volkswagen_Type_2", "Volkswagen Type 2", 8799.5, 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"), 100, "ABCD12345678", "info@salto.rs", "+381 (060) 399 8871", new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2021, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "", null, new DateTimeOffset(new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, false, "https://www.salto.rs/#belgrade-ipa", "Salto IPA", 2.6000000000000001, 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Code",
                table: "Product",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedById",
                table: "Product",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdatedById",
                table: "Product",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
