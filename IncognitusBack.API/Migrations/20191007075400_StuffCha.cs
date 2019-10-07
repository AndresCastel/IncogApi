using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class StuffCha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stuff_Assign_Employee_Register_Employee_RegisterId",
                table: "Stuff_Assign");

            migrationBuilder.RenameColumn(
                name: "Employee_RegisterId",
                table: "Stuff_Assign",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Stuff_Assign_Employee_RegisterId",
                table: "Stuff_Assign",
                newName: "IX_Stuff_Assign_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stuff_Assign_Employee_EmployeeId",
                table: "Stuff_Assign",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stuff_Assign_Employee_EmployeeId",
                table: "Stuff_Assign");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Stuff_Assign",
                newName: "Employee_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_Stuff_Assign_EmployeeId",
                table: "Stuff_Assign",
                newName: "IX_Stuff_Assign_Employee_RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stuff_Assign_Employee_Register_Employee_RegisterId",
                table: "Stuff_Assign",
                column: "Employee_RegisterId",
                principalTable: "Employee_Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
