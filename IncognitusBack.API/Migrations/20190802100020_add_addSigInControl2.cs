using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_addSigInControl2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Stuff_Employee_EmployeeId",
                table: "Employee_Stuff");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Stuff_Type_Register_Type_RegisterId",
                table: "Employee_Stuff");

            migrationBuilder.DropForeignKey(
                name: "FK_Stuff_Assign_Employee_Stuff_Employee_RegisterId",
                table: "Stuff_Assign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_Stuff",
                table: "Employee_Stuff");

            migrationBuilder.RenameTable(
                name: "Employee_Stuff",
                newName: "Employee_Register");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Stuff_Type_RegisterId",
                table: "Employee_Register",
                newName: "IX_Employee_Register_Type_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Stuff_EmployeeId",
                table: "Employee_Register",
                newName: "IX_Employee_Register_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_Register",
                table: "Employee_Register",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Register_Employee_EmployeeId",
                table: "Employee_Register",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Register_Type_Register_Type_RegisterId",
                table: "Employee_Register",
                column: "Type_RegisterId",
                principalTable: "Type_Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stuff_Assign_Employee_Register_Employee_RegisterId",
                table: "Stuff_Assign",
                column: "Employee_RegisterId",
                principalTable: "Employee_Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Register_Employee_EmployeeId",
                table: "Employee_Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Register_Type_Register_Type_RegisterId",
                table: "Employee_Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Stuff_Assign_Employee_Register_Employee_RegisterId",
                table: "Stuff_Assign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_Register",
                table: "Employee_Register");

            migrationBuilder.RenameTable(
                name: "Employee_Register",
                newName: "Employee_Stuff");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Register_Type_RegisterId",
                table: "Employee_Stuff",
                newName: "IX_Employee_Stuff_Type_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Register_EmployeeId",
                table: "Employee_Stuff",
                newName: "IX_Employee_Stuff_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_Stuff",
                table: "Employee_Stuff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Stuff_Employee_EmployeeId",
                table: "Employee_Stuff",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Stuff_Type_Register_Type_RegisterId",
                table: "Employee_Stuff",
                column: "Type_RegisterId",
                principalTable: "Type_Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stuff_Assign_Employee_Stuff_Employee_RegisterId",
                table: "Stuff_Assign",
                column: "Employee_RegisterId",
                principalTable: "Employee_Stuff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
