using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _00_LoginPage.Migrations
{
    /// <inheritdoc />
    public partial class third_recover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingListProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingListProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
