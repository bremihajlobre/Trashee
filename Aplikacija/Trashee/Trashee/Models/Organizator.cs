using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Organizator")]
    public class Organizator
    {
        public Organizator()
        {

        }

        #region Properties
        [Key]
        public int OrganizatorID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Prezime { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Required")]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter your email address")]
        [Display(Name = "Email Address")]
        public string EMail { get; set; }

        [Required]
        public int AdministratorID { get; set; }
        public DateTime DatumDodavanja { get; set; }

        public string ImagePath { get; set; }
        #endregion

        #region Navigation properties
        public virtual Administrator Administrator { get => LazyLoader.Load(this, ref _administrator); set => _administrator = value; }
        public virtual ICollection<OrgNaLok> OrganizovaneAkcije { get => LazyLoader.Load(this, ref _organizovaneAkcije); set => _organizovaneAkcije = value; }
        public virtual ICollection<Obavestenje> Obavestenja { get => LazyLoader.Load(this, ref _obavestenja); set => _obavestenja = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private Administrator _administrator;
        private ICollection<OrgNaLok> _organizovaneAkcije;
        private ICollection<Obavestenje> _obavestenja;

        private Organizator(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
