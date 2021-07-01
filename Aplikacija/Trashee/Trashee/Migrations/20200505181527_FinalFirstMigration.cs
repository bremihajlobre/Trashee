using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class FinalFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lokacijas_Skauts_SkautID",
                table: "Lokacijas");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizators_Administrators_AdministratorID",
                table: "Organizators");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgNaLok_Akcijas_AkcijaID",
                table: "OrgNaLok");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgNaLok_Lokacijas_LokacijaID",
                table: "OrgNaLok");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgNaLok_Organizators_OrganizatorID",
                table: "OrgNaLok");

            migrationBuilder.DropForeignKey(
                name: "FK_SlikeAkcija_Akcijas_AkcijaID",
                table: "SlikeAkcija");

            migrationBuilder.DropForeignKey(
                name: "FK_SlikeLokacija_Lokacijas_LokacijaID",
                table: "SlikeLokacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucestvuje_Akcijas_AkcijaID",
                table: "Ucestvuje");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucestvuje_Skauts_SkautID",
                table: "Ucestvuje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zalbas_Skauts_SkautID",
                table: "Zalbas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrgNaLok",
                table: "OrgNaLok");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zalbas",
                table: "Zalbas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skauts",
                table: "Skauts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizators",
                table: "Organizators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lokacijas",
                table: "Lokacijas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Akcijas",
                table: "Akcijas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "AkcijaID",
                table: "SlikeLokacija");

            migrationBuilder.RenameTable(
                name: "OrgNaLok",
                newName: "Orgnalok");

            migrationBuilder.RenameTable(
                name: "Zalbas",
                newName: "Zalba");

            migrationBuilder.RenameTable(
                name: "Skauts",
                newName: "Skaut");

            migrationBuilder.RenameTable(
                name: "Organizators",
                newName: "Organizator");

            migrationBuilder.RenameTable(
                name: "Lokacijas",
                newName: "Lokacija");

            migrationBuilder.RenameTable(
                name: "Akcijas",
                newName: "Akcija");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "Administrator");

            migrationBuilder.RenameIndex(
                name: "IX_OrgNaLok_OrganizatorID",
                table: "Orgnalok",
                newName: "IX_Orgnalok_OrganizatorID");

            migrationBuilder.RenameIndex(
                name: "IX_OrgNaLok_LokacijaID",
                table: "Orgnalok",
                newName: "IX_Orgnalok_LokacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_OrgNaLok_AkcijaID",
                table: "Orgnalok",
                newName: "IX_Orgnalok_AkcijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Zalbas_SkautID",
                table: "Zalba",
                newName: "IX_Zalba_SkautID");

            migrationBuilder.RenameIndex(
                name: "IX_Organizators_AdministratorID",
                table: "Organizator",
                newName: "IX_Organizator_AdministratorID");

            migrationBuilder.RenameIndex(
                name: "IX_Lokacijas_SkautID",
                table: "Lokacija",
                newName: "IX_Lokacija_SkautID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zalba",
                table: "Zalba",
                column: "ZalbaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skaut",
                table: "Skaut",
                column: "SkautID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizator",
                table: "Organizator",
                column: "OrganizatorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lokacija",
                table: "Lokacija",
                column: "LokacijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Akcija",
                table: "Akcija",
                column: "AkcijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "AdministratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacija_Skaut_SkautID",
                table: "Lokacija",
                column: "SkautID",
                principalTable: "Skaut",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizator_Administrator_AdministratorID",
                table: "Organizator",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orgnalok_Akcija_AkcijaID",
                table: "Orgnalok",
                column: "AkcijaID",
                principalTable: "Akcija",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orgnalok_Lokacija_LokacijaID",
                table: "Orgnalok",
                column: "LokacijaID",
                principalTable: "Lokacija",
                principalColumn: "LokacijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orgnalok_Organizator_OrganizatorID",
                table: "Orgnalok",
                column: "OrganizatorID",
                principalTable: "Organizator",
                principalColumn: "OrganizatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlikeAkcija_Akcija_AkcijaID",
                table: "SlikeAkcija",
                column: "AkcijaID",
                principalTable: "Akcija",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlikeLokacija_Lokacija_LokacijaID",
                table: "SlikeLokacija",
                column: "LokacijaID",
                principalTable: "Lokacija",
                principalColumn: "LokacijaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucestvuje_Akcija_AkcijaID",
                table: "Ucestvuje",
                column: "AkcijaID",
                principalTable: "Akcija",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucestvuje_Skaut_SkautID",
                table: "Ucestvuje",
                column: "SkautID",
                principalTable: "Skaut",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zalba_Skaut_SkautID",
                table: "Zalba",
                column: "SkautID",
                principalTable: "Skaut",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lokacija_Skaut_SkautID",
                table: "Lokacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizator_Administrator_AdministratorID",
                table: "Organizator");

            migrationBuilder.DropForeignKey(
                name: "FK_Orgnalok_Akcija_AkcijaID",
                table: "Orgnalok");

            migrationBuilder.DropForeignKey(
                name: "FK_Orgnalok_Lokacija_LokacijaID",
                table: "Orgnalok");

            migrationBuilder.DropForeignKey(
                name: "FK_Orgnalok_Organizator_OrganizatorID",
                table: "Orgnalok");

            migrationBuilder.DropForeignKey(
                name: "FK_SlikeAkcija_Akcija_AkcijaID",
                table: "SlikeAkcija");

            migrationBuilder.DropForeignKey(
                name: "FK_SlikeLokacija_Lokacija_LokacijaID",
                table: "SlikeLokacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucestvuje_Akcija_AkcijaID",
                table: "Ucestvuje");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucestvuje_Skaut_SkautID",
                table: "Ucestvuje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zalba_Skaut_SkautID",
                table: "Zalba");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zalba",
                table: "Zalba");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skaut",
                table: "Skaut");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizator",
                table: "Organizator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lokacija",
                table: "Lokacija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Akcija",
                table: "Akcija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.RenameTable(
                name: "Orgnalok",
                newName: "OrgNaLok");

            migrationBuilder.RenameTable(
                name: "Zalba",
                newName: "Zalbas");

            migrationBuilder.RenameTable(
                name: "Skaut",
                newName: "Skauts");

            migrationBuilder.RenameTable(
                name: "Organizator",
                newName: "Organizators");

            migrationBuilder.RenameTable(
                name: "Lokacija",
                newName: "Lokacijas");

            migrationBuilder.RenameTable(
                name: "Akcija",
                newName: "Akcijas");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Administrators");

            migrationBuilder.RenameIndex(
                name: "IX_Orgnalok_OrganizatorID",
                table: "OrgNaLok",
                newName: "IX_OrgNaLok_OrganizatorID");

            migrationBuilder.RenameIndex(
                name: "IX_Orgnalok_LokacijaID",
                table: "OrgNaLok",
                newName: "IX_OrgNaLok_LokacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Orgnalok_AkcijaID",
                table: "OrgNaLok",
                newName: "IX_OrgNaLok_AkcijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Zalba_SkautID",
                table: "Zalbas",
                newName: "IX_Zalbas_SkautID");

            migrationBuilder.RenameIndex(
                name: "IX_Organizator_AdministratorID",
                table: "Organizators",
                newName: "IX_Organizators_AdministratorID");

            migrationBuilder.RenameIndex(
                name: "IX_Lokacija_SkautID",
                table: "Lokacijas",
                newName: "IX_Lokacijas_SkautID");

            migrationBuilder.AddColumn<int>(
                name: "AkcijaID",
                table: "SlikeLokacija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrgNaLok",
                table: "OrgNaLok",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zalbas",
                table: "Zalbas",
                column: "ZalbaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skauts",
                table: "Skauts",
                column: "SkautID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizators",
                table: "Organizators",
                column: "OrganizatorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lokacijas",
                table: "Lokacijas",
                column: "LokacijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Akcijas",
                table: "Akcijas",
                column: "AkcijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "AdministratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacijas_Skauts_SkautID",
                table: "Lokacijas",
                column: "SkautID",
                principalTable: "Skauts",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizators_Administrators_AdministratorID",
                table: "Organizators",
                column: "AdministratorID",
                principalTable: "Administrators",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgNaLok_Akcijas_AkcijaID",
                table: "OrgNaLok",
                column: "AkcijaID",
                principalTable: "Akcijas",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgNaLok_Lokacijas_LokacijaID",
                table: "OrgNaLok",
                column: "LokacijaID",
                principalTable: "Lokacijas",
                principalColumn: "LokacijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgNaLok_Organizators_OrganizatorID",
                table: "OrgNaLok",
                column: "OrganizatorID",
                principalTable: "Organizators",
                principalColumn: "OrganizatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlikeAkcija_Akcijas_AkcijaID",
                table: "SlikeAkcija",
                column: "AkcijaID",
                principalTable: "Akcijas",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlikeLokacija_Lokacijas_LokacijaID",
                table: "SlikeLokacija",
                column: "LokacijaID",
                principalTable: "Lokacijas",
                principalColumn: "LokacijaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucestvuje_Akcijas_AkcijaID",
                table: "Ucestvuje",
                column: "AkcijaID",
                principalTable: "Akcijas",
                principalColumn: "AkcijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucestvuje_Skauts_SkautID",
                table: "Ucestvuje",
                column: "SkautID",
                principalTable: "Skauts",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zalbas_Skauts_SkautID",
                table: "Zalbas",
                column: "SkautID",
                principalTable: "Skauts",
                principalColumn: "SkautID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
