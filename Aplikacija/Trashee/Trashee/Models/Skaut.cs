using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Skaut")]
    public class Skaut
    {
        public Skaut()
        {

        }


        #region Properties
        [Key]
        public int SkautID { get; set; }

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

        public DateTime DateReg { get; set; }

        public int Poeni { get; set; } = 0;
        public int SakupljenoSmeca { get; set; } = 0;
        public string Opis { get; set; }

        public string ImagePath { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Navigation properties
        public virtual ICollection<Lokacija> PrijavljeneLokacije { get => LazyLoader.Load(this, ref _prijavljeneLokacije); set => _prijavljeneLokacije = value; }
        public virtual ICollection<Zalba> NapisaneZalbe { get => LazyLoader.Load(this, ref _napisaneZalbe); set => _napisaneZalbe = value; }
        public virtual ICollection<Ucestvuje> Ucestvuje { get => LazyLoader.Load(this, ref _ucestvuje); set => _ucestvuje = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private ICollection<Lokacija> _prijavljeneLokacije;
        private ICollection<Zalba> _napisaneZalbe;
        private ICollection<Ucestvuje> _ucestvuje;

        private Skaut(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }


    }
}
