using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trashee.Models
{
    [Table("Administrator")]
    public class Administrator
    {
        public Administrator()
        {

        }

        #region Properties
        [Key]
        public int AdministratorID { get; set; }

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

        public string ImagePath { get; set; }
        #endregion

        #region Navigation properties
        public virtual ICollection<Organizator> Organizator { get => LazyLoader.Load(this, ref _organizator); set => _organizator = value; }
        #endregion

        private ILazyLoader LazyLoader;
        private ICollection<Organizator> _organizator;

        private Administrator(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
    }
}
