using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_addQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employee_Stuff");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Employee_Stuff",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Employee_Stuff");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employee_Stuff",
                nullable: true);
        }
    }
}
