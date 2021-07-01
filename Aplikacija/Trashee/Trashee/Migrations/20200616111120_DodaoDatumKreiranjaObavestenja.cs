using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trashee.Migrations
{
    public partial class DodaoDatumKreiranjaObavestenja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Orgnalok");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "Obavestenja",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok",
                columns: new[] { "AkcijaID", "OrganizatorID", "LokacijaID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok");

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "Obavestenja");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Orgnalok",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orgnalok",
                table: "Orgnalok",
                column: "ID");
        }
    }
}
