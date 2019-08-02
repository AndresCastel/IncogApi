using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class add_addSigInControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Stuff_Stuff_StuffId",
                table: "Employee_Stuff");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Stuff_StuffId",
                table: "Employee_Stuff");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Employee_Stuff");

            migrationBuilder.DropColumn(
                name: "StuffId",
                table: "Employee_Stuff");

            migrationBuilder.CreateTable(
                name: "Stuff_Assign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StuffId = table.Column<int>(nullable: false),
                    Employee_RegisterId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stuff_Assign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stuff_Assign_Employee_Stuff_Employee_RegisterId",
                        column: x => x.Employee_RegisterId,
                        principalTable: "Employee_Stuff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stuff_Assign_Stuff_StuffId",
                        column: x => x.StuffId,
                        principalTable: "Stuff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stuff_Assign_Employee_RegisterId",
                table: "Stuff_Assign",
                column: "Employee_RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stuff_Assign_StuffId",
                table: "Stuff_Assign",
                column: "StuffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stuff_Assign");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Employee_Stuff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StuffId",
                table: "Employee_Stuff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Stuff_StuffId",
                table: "Employee_Stuff",
                column: "StuffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Stuff_Stuff_StuffId",
                table: "Employee_Stuff",
                column: "StuffId",
                principalTable: "Stuff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
