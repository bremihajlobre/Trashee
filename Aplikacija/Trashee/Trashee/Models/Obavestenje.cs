using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Obavestenja")]
    public class Obavestenje
    {
        public Obavestenje()
        {

        }

        #region Properties
        [Key]
        public int ObavestenjeID { get; set; }
        public int OrganizatorID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumKreiranja { get; set; }
        #endregion

        private ILazyLoader LazyLoader;
        private Organizator _organizator;
        public Obavestenje(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        #region Navigation properties
        public virtual Organizator Organizator { get => LazyLoader.Load(this, ref _organizator); set => _organizator = value; }
        #endregion
    }
}
