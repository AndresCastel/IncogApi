using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncognitusBack.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RosterC",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    Break = table.Column<string>(nullable: true),
                    Precint = table.Column<string>(nullable: true),
                    Zone = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    ShiftNum = table.Column<int>(nullable: false),
                    LabourType = table.Column<string>(nullable: true),
                    Employee = table.Column<string>(nullable: true),
                    Payroll = table.Column<string>(nullable: true),
                    LookedIn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterC", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RosterC");
        }
    }
}
