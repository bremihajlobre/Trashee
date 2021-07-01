using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Lokacija")]
    public class Lokacija
    {
        public Lokacija()
        {

        }

        #region Properties
        [Key]
        public int LokacijaID { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        public int SkautID { get; set; }
        [Required]
        public DateTime DatumPrijavljivanja { get; set; }
        #endregion

        #region Navigation properties
        public Skaut Skaut { get => LazyLoader.Load(this, ref _skaut); set => _skaut = value; }
        public ICollection<SlikeLokacija> SlikeLokacija { get => LazyLoader.Load(this, ref _slikeLokacija); set => _slikeLokacija = value; }
        public virtual OrgNaLok OrgNaLok { get => LazyLoader.Load(this, ref _orgNaLok); set => _orgNaLok = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Skaut _skaut;
        private ICollection<SlikeLokacija> _slikeLokacija;
        private OrgNaLok _orgNaLok;
        private Lokacija(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
