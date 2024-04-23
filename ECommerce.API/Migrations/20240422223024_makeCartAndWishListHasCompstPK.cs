using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class makeCartAndWishListHasCompstPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ApplicationUserId",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Carts");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                columns: new[] { "ApplicationUserId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "ApplicationUserId", "ProductId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId1",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId1",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_ProductId1",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId1",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ApplicationUserId",
                table: "Wishlists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId");
        }
    }
}
