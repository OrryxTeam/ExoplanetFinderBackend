using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoplanetFinderBackend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class experiments_segregation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_Experiments_ExperimentId",
                table: "Observation");

            migrationBuilder.RenameColumn(
                name: "ExperimentId",
                table: "Observation",
                newName: "CustomWaveExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Observation_ExperimentId",
                table: "Observation",
                newName: "IX_Observation_CustomWaveExperimentId");

            migrationBuilder.RenameColumn(
                name: "PlanetType",
                table: "Assumptions",
                newName: "PlanetName");

            migrationBuilder.CreateTable(
                name: "CustomWaveExperiments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomWaveExperiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomWaveExperiments_Experiments_Id",
                        column: x => x.Id,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnownStarExperiments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StarId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnownStarExperiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnownStarExperiments_Experiments_Id",
                        column: x => x.Id,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_CustomWaveExperiments_CustomWaveExperimentId",
                table: "Observation",
                column: "CustomWaveExperimentId",
                principalTable: "CustomWaveExperiments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_CustomWaveExperiments_CustomWaveExperimentId",
                table: "Observation");

            migrationBuilder.DropTable(
                name: "CustomWaveExperiments");

            migrationBuilder.DropTable(
                name: "KnownStarExperiments");

            migrationBuilder.RenameColumn(
                name: "CustomWaveExperimentId",
                table: "Observation",
                newName: "ExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Observation_CustomWaveExperimentId",
                table: "Observation",
                newName: "IX_Observation_ExperimentId");

            migrationBuilder.RenameColumn(
                name: "PlanetName",
                table: "Assumptions",
                newName: "PlanetType");

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_Experiments_ExperimentId",
                table: "Observation",
                column: "ExperimentId",
                principalTable: "Experiments",
                principalColumn: "Id");
        }
    }
}
