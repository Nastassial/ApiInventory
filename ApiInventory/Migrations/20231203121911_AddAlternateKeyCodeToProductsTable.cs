using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddAlternateKeyCodeToProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Products_Code",
                table: "Products",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Products_Code",
                table: "Products");
        }
    }
}
