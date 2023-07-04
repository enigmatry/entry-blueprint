using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enigmatry.Blueprint.Data.Migrations.Migrations
{
    public partial class AddRolesAndPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("65fb31fa-ff40-4bd5-8fe6-755122b6428b"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("67a75734-d04a-4661-95d9-24879122c901"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("767c974c-8c07-43f5-8c6f-0cc1eef3c65a"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"),
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"),
                column: "CreatedById",
                value: null);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0"), "SystemAdmin" });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name",
                table: "Permission",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Name", "UpdatedById", "UpdatedOn", "UserName" },
                values: new object[] { new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), null, new DateTimeOffset(new DateTime(2019, 5, 6, 14, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Test" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("04c45383-1372-4312-bb38-5edfa569db66"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1de0818e-04d7-4435-98af-114b81aff0d0"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("65fb31fa-ff40-4bd5-8fe6-755122b6428b"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("67a75734-d04a-4661-95d9-24879122c901"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("767c974c-8c07-43f5-8c6f-0cc1eef3c65a"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7a4ce0d5-471b-4d35-89f2-b6c7a68350c0"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a84132e7-cfc9-4766-8da0-b1d9e549de57"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("da8eb2a9-1f8a-4c41-8df9-10a1fc59305b"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc7047b0-0d35-4cbc-9424-d907ae5a25f4"),
                column: "CreatedById",
                value: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"));
        }
    }
}
