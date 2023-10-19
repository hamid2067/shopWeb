using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addvcop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_weblog",
                table: "weblog");

            migrationBuilder.DropColumn(
                name: "weblogId",
                table: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "weblog",
                newName: "Weblog");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Weblog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "urlProduct",
                table: "Weblog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weblog",
                table: "Weblog",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Weblog_categoryId",
                table: "Weblog",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weblog_ProductCategory_categoryId",
                table: "Weblog",
                column: "categoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weblog_ProductCategory_categoryId",
                table: "Weblog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weblog",
                table: "Weblog");

            migrationBuilder.DropIndex(
                name: "IX_Weblog_categoryId",
                table: "Weblog");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Weblog");

            migrationBuilder.DropColumn(
                name: "urlProduct",
                table: "Weblog");

            migrationBuilder.RenameTable(
                name: "Weblog",
                newName: "weblog");

            migrationBuilder.AddColumn<int>(
                name: "weblogId",
                table: "ProductCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_weblog",
                table: "weblog",
                column: "Id");
        }
    }
}
