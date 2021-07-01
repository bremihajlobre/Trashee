using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class DodaoModelObavestenja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orgnalok_OrganizatorID",
                table: "Orgnalok");

            migrationBuilder.AlterColumn<int>(
                name: "Uspesna",
                table: "Akcija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BrojUcesnika",
                table: "Akcija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Obavestenja",
                columns: table => new
                {
                    ObavestenjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizatorID = table.Column<int>(type: "int", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavestenja", x => x.ObavestenjeID);
                    table.ForeignKey(
                        name: "FK_Obavestenja_Organizator_OrganizatorID",
                        column: x => x.OrganizatorID,
                        principalTable: "Organizator",
                        principalColumn: "OrganizatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orgnalok_OrganizatorID",
                table: "Orgnalok",
                column: "OrganizatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Obavestenja_OrganizatorID",
                table: "Obavestenja",
                column: "OrganizatorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavestenja");

            migrationBuilder.DropIndex(
                name: "IX_Orgnalok_OrganizatorID",
                table: "Orgnalok");

            migrationBuilder.AlterColumn<int>(
                name: "Uspesna",
                table: "Akcija",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BrojUcesnika",
                table: "Akcija",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orgnalok_OrganizatorID",
                table: "Orgnalok",
                column: "OrganizatorID",
                unique: true);
        }
    }
}
