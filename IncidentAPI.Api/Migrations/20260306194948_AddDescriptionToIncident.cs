using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncidentAPI.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToIncident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Incidents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Incidents");
        }
    }
}
