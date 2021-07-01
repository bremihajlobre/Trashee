using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Ucestvuje")]
    public class Ucestvuje
    {
        public Ucestvuje()
        {

        }


        #region Properties
        //[Key]
        //public int UcestvujeID { get; set; }
        public int SkautID { get; set; }
        public int AkcijaID { get; set; }

        #endregion

        #region Navigation properties
        //[ForeignKey("SkautID")]
        //[InverseProperty("Ucestvuje")]
        public virtual Skaut Skaut { get => LazyLoader.Load(this, ref _skaut); set => _skaut = value; }
        //[ForeignKey("AkcijaID")]
        //[InverseProperty("Ucestvuje")]
        public virtual Akcija Akcija { get => LazyLoader.Load(this, ref _akcija); set => _akcija = value; }
        #endregion

        private Ucestvuje(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public ILazyLoader LazyLoader { get; set; }
        private Skaut _skaut;
        private Akcija _akcija;
    }
}
