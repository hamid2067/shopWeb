using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class add44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Description", "Hash", "ModifiedByUserId", "ModifiedDateTime", "Name" },
                values: new object[] { 1, null, null, "abcd", null, null, null, "مبل" });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "categoryName" },
                values: new object[] { 2, null, null, null, null, null, "abcd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    groupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 1, null, null, null, null, null, "icon-menu", "داشبورد", "/admin/dashbord/index", 0 });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 2, null, null, null, null, null, "icon-file-text", "محصولات", "/admin/product", 0 });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 3, null, null, null, null, null, "icon-folder", "افزودن محصولات", "/admin/product", 2 });
        }
    }
}
