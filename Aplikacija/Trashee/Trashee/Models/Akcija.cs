using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Akcija")]
    public class Akcija
    {
        public Akcija()
        {
        }

        #region Properties
        [Key]
        public int AkcijaID { get; set; }
        //public int Uspesna { get; set; }

        [DataType("nvarchar(255)")]
        public string Status { get; set; }
        public int BrojUcesnika { get; set; }
        #endregion

        #region Navigation properties
        public virtual ICollection<SlikeAkcija> SlikeAkcija { get => LazyLoader.Load(this, ref _slikeAkcija); set => _slikeAkcija = value; }
        public virtual ICollection<Ucestvuje> Ucestvuje { get => LazyLoader.Load(this, ref _ucestvuje); set => _ucestvuje = value; }
        public OrgNaLok OrgNaLok { get => LazyLoader.Load(this, ref _orgNaLok); set => _orgNaLok = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private ICollection<Ucestvuje> _ucestvuje;
        private ICollection<SlikeAkcija> _slikeAkcija;
        private OrgNaLok _orgNaLok;

        private Akcija(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
