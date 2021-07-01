using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class FirstRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    AdministratorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.AdministratorID);
                });

            migrationBuilder.CreateTable(
                name: "Akcijas",
                columns: table => new
                {
                    AkcijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uspesna = table.Column<int>(type: "int", nullable: false),
                    BrojUcesnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akcijas", x => x.AkcijaID);
                });

            migrationBuilder.CreateTable(
                name: "Skauts",
                columns: table => new
                {
                    SkautID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poeni = table.Column<int>(type: "int", nullable: false),
                    SakupljenoSmeca = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skauts", x => x.SkautID);
                });

            migrationBuilder.CreateTable(
                name: "Organizators",
                columns: table => new
                {
                    OrganizatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdministratorID = table.Column<int>(type: "int", nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizators", x => x.OrganizatorID);
                    table.ForeignKey(
                        name: "FK_Organizators_Administrators_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "Administrators",
                        principalColumn: "AdministratorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlikeAkcija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkcijaID = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlikeAkcija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SlikeAkcija_Akcijas_AkcijaID",
                        column: x => x.AkcijaID,
                        principalTable: "Akcijas",
                        principalColumn: "AkcijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lokacijas",
                columns: table => new
                {
                    LokacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    North = table.Column<float>(type: "real", nullable: false),
                    East = table.Column<float>(type: "real", nullable: false),
                    SkautID = table.Column<int>(type: "int", nullable: false),
                    DatumPrijavljivanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacijas", x => x.LokacijaID);
                    table.ForeignKey(
                        name: "FK_Lokacijas_Skauts_SkautID",
                        column: x => x.SkautID,
                        principalTable: "Skauts",
                        principalColumn: "SkautID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ucestvuje",
                columns: table => new
                {
                    UcestvujeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkautID = table.Column<int>(type: "int", nullable: false),
                    AkcijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucestvuje", x => x.UcestvujeID);
                    table.ForeignKey(
                        name: "FK_Ucestvuje_Akcijas_AkcijaID",
                        column: x => x.AkcijaID,
                        principalTable: "Akcijas",
                        principalColumn: "AkcijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ucestvuje_Skauts_SkautID",
                        column: x => x.SkautID,
                        principalTable: "Skauts",
                        principalColumn: "SkautID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zalbas",
                columns: table => new
                {
                    ZalbaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkautID = table.Column<int>(type: "int", nullable: false),
                    OpisZalba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrijavljeniUsername = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalbas", x => x.ZalbaID);
                    table.ForeignKey(
                        name: "FK_Zalbas_Skauts_SkautID",
                        column: x => x.SkautID,
                        principalTable: "Skauts",
                        principalColumn: "SkautID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrgNaLok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkcijaID = table.Column<int>(type: "int", nullable: false),
                    OrganizatorID = table.Column<int>(type: "int", nullable: false),
                    LokacijaID = table.Column<int>(type: "int", nullable: false),
                    DatumOrganizovanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgNaLok", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrgNaLok_Akcijas_AkcijaID",
                        column: x => x.AkcijaID,
                        principalTable: "Akcijas",
                        principalColumn: "AkcijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgNaLok_Lokacijas_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacijas",
                        principalColumn: "LokacijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrgNaLok_Organizators_OrganizatorID",
                        column: x => x.OrganizatorID,
                        principalTable: "Organizators",
                        principalColumn: "OrganizatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlikeLokacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkcijaID = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LokacijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlikeLokacija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SlikeLokacija_Lokacijas_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacijas",
                        principalColumn: "LokacijaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lokacijas_SkautID",
                table: "Lokacijas",
                column: "SkautID");

            migrationBuilder.CreateIndex(
                name: "IX_Organizators_AdministratorID",
                table: "Organizators",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_OrgNaLok_AkcijaID",
                table: "OrgNaLok",
                column: "AkcijaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgNaLok_LokacijaID",
                table: "OrgNaLok",
                column: "LokacijaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgNaLok_OrganizatorID",
                table: "OrgNaLok",
                column: "OrganizatorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlikeAkcija_AkcijaID",
                table: "SlikeAkcija",
                column: "AkcijaID");

            migrationBuilder.CreateIndex(
                name: "IX_SlikeLokacija_LokacijaID",
                table: "SlikeLokacija",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuje_AkcijaID",
                table: "Ucestvuje",
                column: "AkcijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuje_SkautID",
                table: "Ucestvuje",
                column: "SkautID");

            migrationBuilder.CreateIndex(
                name: "IX_Zalbas_SkautID",
                table: "Zalbas",
                column: "SkautID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrgNaLok");

            migrationBuilder.DropTable(
                name: "SlikeAkcija");

            migrationBuilder.DropTable(
                name: "SlikeLokacija");

            migrationBuilder.DropTable(
                name: "Ucestvuje");

            migrationBuilder.DropTable(
                name: "Zalbas");

            migrationBuilder.DropTable(
                name: "Organizators");

            migrationBuilder.DropTable(
                name: "Lokacijas");

            migrationBuilder.DropTable(
                name: "Akcijas");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Skauts");
        }
    }
}
