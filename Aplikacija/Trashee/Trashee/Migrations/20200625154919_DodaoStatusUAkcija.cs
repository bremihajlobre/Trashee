using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class DodaoStatusUAkcija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uspesna",
                table: "Akcija");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Akcija",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Akcija");

            migrationBuilder.AddColumn<int>(
                name: "Uspesna",
                table: "Akcija",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
