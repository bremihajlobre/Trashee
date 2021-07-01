using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class AddRazlogToZalba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Razlog",
                table: "Zalba",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Razlog",
                table: "Zalba");
        }
    }
}
