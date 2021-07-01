using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("SlikeAkcija")]
    public class SlikeAkcija
    {
        public SlikeAkcija()
        {

        }


        #region Properies
        [Key]
        public int ID { get; set; }
        public int AkcijaID { get; set; }
        [Required]
        public string Slika { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Akcija Akcija { get => LazyLoader.Load(this, ref _akcija); set => _akcija = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Akcija _akcija;

        private SlikeAkcija(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

    }
}
