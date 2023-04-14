using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetapp.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FitnessTracker",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Workout_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Steps = table.Column<int>(nullable: false),
                    Distance_km = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    CaloriesBurned = table.Column<int>(nullable: false),
                    HeartRate = table.Column<int>(nullable: false),
                    SleepDuration = table.Column<decimal>(type: "decimal(4, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessTracker", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessTracker");
        }
    }
}
