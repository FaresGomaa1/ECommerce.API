using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class firstOne1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Sizes_SizeName",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_SizeName",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "ProductColors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "ProductColors",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_SizeName",
                table: "ProductColors",
                column: "SizeName");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Sizes_SizeName",
                table: "ProductColors",
                column: "SizeName",
                principalTable: "Sizes",
                principalColumn: "SizeName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
