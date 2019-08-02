using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_SignInOff2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type_RegisterId",
                table: "Employee_Stuff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Stuff_Type_RegisterId",
                table: "Employee_Stuff",
                column: "Type_RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Stuff_Type_Register_Type_RegisterId",
                table: "Employee_Stuff",
                column: "Type_RegisterId",
                principalTable: "Type_Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Stuff_Type_Register_Type_RegisterId",
                table: "Employee_Stuff");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Stuff_Type_RegisterId",
                table: "Employee_Stuff");

            migrationBuilder.DropColumn(
                name: "Type_RegisterId",
                table: "Employee_Stuff");
        }
    }
}
