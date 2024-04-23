using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class makeCartAndWishListHasCompstPK1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId1",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ProductId1",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId1",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Wishlists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductId1",
                table: "Wishlists",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId1",
                table: "Carts",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId1",
                table: "Carts",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductId1",
                table: "Wishlists",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
