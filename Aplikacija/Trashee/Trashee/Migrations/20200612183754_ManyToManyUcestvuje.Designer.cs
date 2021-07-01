﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trashee.Models;

namespace Trashee.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200612183754_ManyToManyUcestvuje")]
    partial class ManyToManyUcestvuje
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.3.20181.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trashee.Models.Administrator", b =>
                {
                    b.Property<int>("AdministratorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("AdministratorID");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("Trashee.Models.Akcija", b =>
                {
                    b.Property<int>("AkcijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojUcesnika")
                        .HasColumnType("int");

                    b.Property<int>("Uspesna")
                        .HasColumnType("int");

                    b.HasKey("AkcijaID");

                    b.ToTable("Akcija");
                });

            modelBuilder.Entity("Trashee.Models.Lokacija", b =>
                {
                    b.Property<int>("LokacijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPrijavljivanja")
                        .HasColumnType("datetime2");

                    b.Property<float>("East")
                        .HasColumnType("real");

                    b.Property<float>("North")
                        .HasColumnType("real");

                    b.Property<int>("SkautID")
                        .HasColumnType("int");

                    b.HasKey("LokacijaID");

                    b.HasIndex("SkautID");

                    b.ToTable("Lokacija");
                });

            modelBuilder.Entity("Trashee.Models.OrgNaLok", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AkcijaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumOrganizovanja")
                        .HasColumnType("datetime2");

                    b.Property<int>("LokacijaID")
                        .HasColumnType("int");

                    b.Property<int>("OrganizatorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AkcijaID")
                        .IsUnique();

                    b.HasIndex("LokacijaID")
                        .IsUnique();

                    b.HasIndex("OrganizatorID")
                        .IsUnique();

                    b.ToTable("Orgnalok");
                });

            modelBuilder.Entity("Trashee.Models.Organizator", b =>
                {
                    b.Property<int>("OrganizatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumDodavanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("OrganizatorID");

                    b.HasIndex("AdministratorID");

                    b.ToTable("Organizator");
                });

            modelBuilder.Entity("Trashee.Models.Skaut", b =>
                {
                    b.Property<int>("SkautID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateReg")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Poeni")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SakupljenoSmeca")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SkautID");

                    b.ToTable("Skaut");
                });

            modelBuilder.Entity("Trashee.Models.SlikeAkcija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AkcijaID")
                        .HasColumnType("int");

                    b.Property<string>("Slika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AkcijaID");

                    b.ToTable("SlikeAkcija");
                });

            modelBuilder.Entity("Trashee.Models.SlikeLokacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LokacijaID")
                        .HasColumnType("int");

                    b.Property<string>("Slika")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("LokacijaID");

                    b.ToTable("SlikeLokacija");
                });

            modelBuilder.Entity("Trashee.Models.Ucestvuje", b =>
                {
                    b.Property<int>("SkautID")
                        .HasColumnType("int");

                    b.Property<int>("AkcijaID")
                        .HasColumnType("int");

                    b.HasKey("SkautID", "AkcijaID");

                    b.HasIndex("AkcijaID");

                    b.ToTable("Ucestvuje");
                });

            modelBuilder.Entity("Trashee.Models.Zalba", b =>
                {
                    b.Property<int>("ZalbaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OpisZalba")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrijavljeniUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Razlog")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkautID")
                        .HasColumnType("int");

                    b.HasKey("ZalbaID");

                    b.HasIndex("SkautID");

                    b.ToTable("Zalba");
                });

            modelBuilder.Entity("Trashee.Models.Lokacija", b =>
                {
                    b.HasOne("Trashee.Models.Skaut", "Skaut")
                        .WithMany("PrijavljeneLokacije")
                        .HasForeignKey("SkautID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trashee.Models.OrgNaLok", b =>
                {
                    b.HasOne("Trashee.Models.Akcija", "Akcija")
                        .WithOne("OrgNaLok")
                        .HasForeignKey("Trashee.Models.OrgNaLok", "AkcijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trashee.Models.Lokacija", "Lokacija")
                        .WithOne("OrgNaLok")
                        .HasForeignKey("Trashee.Models.OrgNaLok", "LokacijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trashee.Models.Organizator", "Organizator")
                        .WithOne("OrganizovaneAkcije")
                        .HasForeignKey("Trashee.Models.OrgNaLok", "OrganizatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trashee.Models.Organizator", b =>
                {
                    b.HasOne("Trashee.Models.Administrator", "Administrator")
                        .WithMany("Organizators")
                        .HasForeignKey("AdministratorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trashee.Models.SlikeAkcija", b =>
                {
                    b.HasOne("Trashee.Models.Akcija", "Akcija")
                        .WithMany("SlikeAkcija")
                        .HasForeignKey("AkcijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trashee.Models.SlikeLokacija", b =>
                {
                    b.HasOne("Trashee.Models.Lokacija", "Lokacija")
                        .WithMany("SlikeLokacija")
                        .HasForeignKey("LokacijaID");
                });

            modelBuilder.Entity("Trashee.Models.Ucestvuje", b =>
                {
                    b.HasOne("Trashee.Models.Akcija", "Akcija")
                        .WithMany("Ucestvuje")
                        .HasForeignKey("AkcijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trashee.Models.Skaut", "Skaut")
                        .WithMany("Ucestvuje")
                        .HasForeignKey("SkautID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trashee.Models.Zalba", b =>
                {
                    b.HasOne("Trashee.Models.Skaut", "Skaut")
                        .WithMany("NapisaneZalbe")
                        .HasForeignKey("SkautID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
