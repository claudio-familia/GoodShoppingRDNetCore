using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodShoppingRD.Migrations
{
    public partial class AddColumnNameToSupermarketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Supermarkets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Supermarkets");
        }
    }
}
