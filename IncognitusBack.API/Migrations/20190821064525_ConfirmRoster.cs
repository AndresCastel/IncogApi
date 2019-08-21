using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class ConfirmRoster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirm",
                table: "RosterC",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirm",
                table: "RosterC");
        }
    }
}
