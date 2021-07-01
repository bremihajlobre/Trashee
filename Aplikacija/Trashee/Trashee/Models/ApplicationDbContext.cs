using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Skaut> Skauts { get; set; }
        public DbSet<Akcija> Akcijas { get; set; }
        public DbSet<Organizator> Organizators { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<SlikeAkcija> SlikeAkcija { get; set; }
        public DbSet<Lokacija> Lokacijas { get; set; }
        public DbSet<SlikeLokacija> SlikeLokacija { get; set; }
        public DbSet<Zalba> Zalbas { get; set; }
        public DbSet<Ucestvuje> Ucestvuje { get; set; }
        public DbSet<OrgNaLok> OrgNaLok { get; set; }
        public DbSet<Obavestenje> Obavestenja { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ucestvuje>().HasKey(SkAk =>
            new { SkAk.SkautID, SkAk.AkcijaID });

            modelBuilder.Entity<OrgNaLok>().HasKey(AkOrgLok =>
            new { AkOrgLok.AkcijaID, AkOrgLok.OrganizatorID, AkOrgLok.LokacijaID });

            modelBuilder.Entity<Akcija>()
                .Property(p => p.BrojUcesnika)
                .HasDefaultValue(0);
        }
    }
}
