using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class submenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDateTime", "Hash", "ModifiedByUserId", "ModifiedDateTime", "NameIcon", "NameMenu", "PageAddress", "ParentId" },
                values: new object[] { 3, null, null, null, null, null, "icon-folder", "افزودن محصولات", "/admin/product", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
