using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Orgnalok")]
    public class OrgNaLok
    {
        public OrgNaLok()
        {

        }

        #region Properties
        [Key]
        //public int ID { get; set; }
        public int AkcijaID { get; set; }
        public int OrganizatorID { get; set; }
        public int LokacijaID { get; set; }
        public DateTime DatumOrganizovanja { get; set; }
        #endregion

        #region Navigation properties
        public virtual Akcija Akcija { get => LazyLoader.Load(this, ref _akcija); set => _akcija = value; }
        public virtual Organizator Organizator { get => LazyLoader.Load(this, ref _organizator); set => _organizator = value; }
        public virtual Lokacija Lokacija { get => LazyLoader.Load(this, ref _lokacija); set => _lokacija = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Akcija _akcija;
        private Organizator _organizator;
        private Lokacija _lokacija;

        private OrgNaLok(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
