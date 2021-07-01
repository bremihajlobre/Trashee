using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class ManyToManyUcestvuje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ucestvuje",
                table: "Ucestvuje");

            migrationBuilder.DropIndex(
                name: "IX_Ucestvuje_SkautID",
                table: "Ucestvuje");

            migrationBuilder.DropColumn(
                name: "UcestvujeID",
                table: "Ucestvuje");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ucestvuje",
                table: "Ucestvuje",
                columns: new[] { "SkautID", "AkcijaID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ucestvuje",
                table: "Ucestvuje");

            migrationBuilder.AddColumn<int>(
                name: "UcestvujeID",
                table: "Ucestvuje",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ucestvuje",
                table: "Ucestvuje",
                column: "UcestvujeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuje_SkautID",
                table: "Ucestvuje",
                column: "SkautID");
        }
    }
}
