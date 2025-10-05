using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoplanetFinderBackend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class experiment_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Experiments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Experiments");
        }
    }
}
