using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Trashee.Models;

namespace Trashee.Pages
{
    public class TableModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TableModel(ApplicationDbContext db)
        {
            _db = db;
        }


        #region Properties
        public class ViewModel
        {
            public string ImeSkauta { get; set; }
            public string PrezimeSkauta { get; set; }
            public int BrojAkcija { get; set; }
            public int BrojLokacija { get; set; }
            public DateTime DatumRegistrovanja { get; set; }
            public int Poeni { get; set; }
            public MedaljeLokacije MedaljaLokacije { get; set; }
            public MedaljeAkcije MedaljaAkcije { get; set; }
            public MedaljeRegistrovanje MedaljaRegistovanje { get; set; }
        }

        public ICollection<Skaut> Skauti { get; set; }
        public IEnumerable<ViewModel> RangListaData { get; set; }

        public enum MedaljeLokacije
        {
            Zlatna,
            Srebrna,
            Bronzana,
            Nema
        }

        public enum MedaljeAkcije
        {
            Zlatna,
            Srebrna,
            Bronzana,
            Nema
        }

        public enum MedaljeRegistrovanje
        {
            Zlatna,
            Srebrna,
            Bronzana,
            Nema
        }

        #endregion




        public async Task<IActionResult> OnGet()
        {
            this.Skauti = await _db.Skauts.ToListAsync();
            IzracunajPoeneZaSkaute(this.Skauti);

            this.RangListaData = Skauti.Select(s => new ViewModel
            {
                ImeSkauta = s.Ime,
                PrezimeSkauta = s.Prezime,
                BrojAkcija = IzracunajBrojAkcijaZaSkauta(s),
                BrojLokacija = IzracunajBrojLokacijaZaSkauta(s),
                DatumRegistrovanja = s.DateReg.Date,
                Poeni = s.Poeni,
                MedaljaAkcije = DodeliMedaljuZaAkcije(s),
                MedaljaLokacije = DodeliMedaljuZaLokacije(s),
                MedaljaRegistovanje = DodeliMedaljuZaGodineClanstva(s.DateReg)
            })
                .OrderByDescending(k => k.Poeni)
                .ToList().Take(10);

            return Page();
        }

        private int IzracunajBrojAkcijaZaSkauta(Skaut skaut)
        {
            return skaut.Ucestvuje.Where(s => s.SkautID == skaut.SkautID).Count();
        }

        private int IzracunajBrojLokacijaZaSkauta(Skaut skaut)
        {
            return skaut.PrijavljeneLokacije.Count();
        }




        #region Methods for caluclating
        private void IzracunajPoeneZaSkaute(ICollection<Skaut> skauti)
        {
            foreach (Skaut skaut in skauti)
            {
                skaut.Poeni = IzracunajPoeneZaJednogSkauta(skaut);
            }

            _db.SaveChanges();
        }

        private int IzracunajPoeneZaJednogSkauta(Skaut skaut)
        {
            int poeni = IzracunajBrojAkcijaZaSkauta(skaut) * 20
                + IzracunajBrojLokacijaZaSkauta(skaut) * 20
                + Years(skaut.DateReg, DateTime.Now) * 100;

            return poeni;
        }

        public int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);

        }

        private MedaljeAkcije DodeliMedaljuZaAkcije(Skaut skaut)
        {
            var brojAkcija = IzracunajBrojAkcijaZaSkauta(skaut);

            if (brojAkcija >= 300)
            {
                return MedaljeAkcije.Zlatna;
            }
            else if (brojAkcija >= 200)
            {
                return MedaljeAkcije.Srebrna;
            }
            else if (brojAkcija >= 100)
            {
                return MedaljeAkcije.Bronzana;
            }
            else
            {
                return MedaljeAkcije.Nema;
            }
        }

        private MedaljeLokacije DodeliMedaljuZaLokacije(Skaut skaut)
        {
            var brojLokacija = IzracunajBrojAkcijaZaSkauta(skaut);

            if (brojLokacija >= 300)
            {
                return MedaljeLokacije.Zlatna;
            }
            else if (brojLokacija >= 200)
            {
                return MedaljeLokacije.Srebrna;
            }
            else if(brojLokacija >= 100)
            {
                return MedaljeLokacije.Bronzana;
            }
            else
            {
                return MedaljeLokacije.Nema;
            }
        }


        private MedaljeRegistrovanje DodeliMedaljuZaGodineClanstva(DateTime datumRegistrovanja)
        {
            var godineClanstva = Years(datumRegistrovanja, DateTime.Now);

            if (godineClanstva >= 3)
            {
                return MedaljeRegistrovanje.Zlatna;
            }
            else if (godineClanstva >= 2)
            {
                return MedaljeRegistrovanje.Srebrna;
            }
            else if(godineClanstva >= 1)
            {
                return MedaljeRegistrovanje.Bronzana;
            }
            else
            {
                return MedaljeRegistrovanje.Nema;
            }
        }


        #endregion

    }
}