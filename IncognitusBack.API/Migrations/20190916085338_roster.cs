using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class roster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RosterId",
                table: "Employee_Register",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Register_RosterId",
                table: "Employee_Register",
                column: "RosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Register_RosterC_RosterId",
                table: "Employee_Register",
                column: "RosterId",
                principalTable: "RosterC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Register_RosterC_RosterId",
                table: "Employee_Register");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Register_RosterId",
                table: "Employee_Register");

            migrationBuilder.DropColumn(
                name: "RosterId",
                table: "Employee_Register");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
