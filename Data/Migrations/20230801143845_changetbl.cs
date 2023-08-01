using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class changetbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PIP_ProductId",
                table: "PIP",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PIP_Product_ProductId",
                table: "PIP",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PIP_Product_ProductId",
                table: "PIP");

            migrationBuilder.DropIndex(
                name: "IX_PIP_ProductId",
                table: "PIP");
        }
    }
}
