using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_rol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payroll",
                table: "Employee",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Employee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RolId",
                table: "Employee",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Rol_RolId",
                table: "Employee",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Rol_RolId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Employee_RolId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Payroll",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Employee");
        }
    }
}
