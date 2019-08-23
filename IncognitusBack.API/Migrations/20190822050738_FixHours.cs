using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class FixHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignIn",
                table: "Employee_Register");

            migrationBuilder.RenameColumn(
                name: "Signoff",
                table: "Employee_Register",
                newName: "Day");

            migrationBuilder.AlterColumn<bool>(
                name: "LookedIn",
                table: "RosterC",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Break",
                table: "RosterC",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "RosterC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Employee_Register",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Employee_Register",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "RosterC");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Employee_Register");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Employee_Register");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Employee_Register",
                newName: "Signoff");

            migrationBuilder.AlterColumn<string>(
                name: "LookedIn",
                table: "RosterC",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Break",
                table: "RosterC",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "SignIn",
                table: "Employee_Register",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
