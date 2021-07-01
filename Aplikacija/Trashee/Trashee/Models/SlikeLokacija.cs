using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("SlikeLokacija")]
    public class SlikeLokacija
    {
        public SlikeLokacija()
        {

        }

        #region Properties
        [Key]
        public int ID { get; set; }
        [Required]
        public string Slika { get; set; }
        #endregion

        #region Navigation properties
        public virtual Lokacija Lokacija { get => LazyLoader.Load(this, ref _lokacija); set => _lokacija = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Lokacija _lokacija;

        private SlikeLokacija(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
