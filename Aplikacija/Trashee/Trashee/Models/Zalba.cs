using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Zalba")]
    public class Zalba
    {
        public Zalba()
        {

        }

        #region Properties
        [Key]
        public int ZalbaID { get; set; }
        public int SkautID { get; set; }
        public string OpisZalba { get; set; }
        public string PrijavljeniUsername { get; set; }
        public string Razlog { get; set; }
        #endregion

        #region Navigation properties
        public virtual Skaut Skaut { get => LazyLoader.Load(this, ref _skaut); set => _skaut = value; }

        [NotMapped]
        public virtual Skaut PrijavljeniSkaut { get => LazyLoader.Load(this, ref _prijavljeniSkaut); set => _prijavljeniSkaut = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Skaut _skaut;
        private Skaut _prijavljeniSkaut;

        private Zalba(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
