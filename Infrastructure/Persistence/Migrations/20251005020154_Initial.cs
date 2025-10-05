using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoplanetFinderBackend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conclusion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conclusion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanetType = table.Column<string>(type: "text", nullable: false),
                    Probability = table.Column<double>(type: "double precision", nullable: false),
                    OrbitDays = table.Column<double>(type: "double precision", nullable: false),
                    DistanceToStar = table.Column<double>(type: "double precision", nullable: false),
                    EarthRadius = table.Column<double>(type: "double precision", nullable: false),
                    ConclusionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assumptions_Conclusion_ConclusionId",
                        column: x => x.ConclusionId,
                        principalTable: "Conclusion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConclusionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConductedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_Conclusion_ConclusionId",
                        column: x => x.ConclusionId,
                        principalTable: "Conclusion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<double>(type: "double precision", nullable: false),
                    Flux = table.Column<double>(type: "double precision", nullable: false),
                    FluxError = table.Column<double>(type: "double precision", nullable: false),
                    Quality = table.Column<double>(type: "double precision", nullable: false),
                    CentroidCol = table.Column<double>(type: "double precision", nullable: false),
                    CentroidRow = table.Column<double>(type: "double precision", nullable: false),
                    SapFlux = table.Column<double>(type: "double precision", nullable: false),
                    Background = table.Column<double>(type: "double precision", nullable: false),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observation_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assumptions_ConclusionId",
                table: "Assumptions",
                column: "ConclusionId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_ConclusionId",
                table: "Experiments",
                column: "ConclusionId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_ExperimentId",
                table: "Observation",
                column: "ExperimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assumptions");

            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "Conclusion");
        }
    }
}
