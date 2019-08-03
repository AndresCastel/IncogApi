using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_1RegisterSignIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Employee_Register",
                newName: "Signoff");

            migrationBuilder.AddColumn<int>(
                name: "Break",
                table: "Employee_Register",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignIn",
                table: "Employee_Register",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Break",
                table: "Employee_Register");

            migrationBuilder.DropColumn(
                name: "SignIn",
                table: "Employee_Register");

            migrationBuilder.RenameColumn(
                name: "Signoff",
                table: "Employee_Register",
                newName: "TransactionDate");
        }
    }
}
