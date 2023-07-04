using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 1, null, null, null, null, null, "icon-menu", "داشبورد", "/admin/dashbord/index", 0 });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 2, null, null, null, null, null, "icon-file-text", "محصولات", "/admin/product", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
