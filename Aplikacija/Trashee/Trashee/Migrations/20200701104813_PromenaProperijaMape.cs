using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class PromenaProperijaMape : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "East",
                table: "Lokacija");

            migrationBuilder.DropColumn(
                name: "North",
                table: "Lokacija");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Lokacija",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Lokacija",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Lokacija");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Lokacija");

            migrationBuilder.AddColumn<float>(
                name: "East",
                table: "Lokacija",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "North",
                table: "Lokacija",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
