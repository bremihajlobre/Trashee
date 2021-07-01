using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Trashee.Models;

namespace Trashee.Pages
{
    [Authorize]
    public class PocetnaModel : PageModel
    {
        #region DI
        private readonly ApplicationDbContext _db;
        public PocetnaModel(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Properties
        public enum FilterOptions
        {
            Svi,
            Organizator,
            Skaut
        }
        public Skaut Skaut { get; set; }
        public Organizator Organizator { get; set; }
        public Administrator Administrator { get; set; }
        [BindProperty]
        public ICollection<Obavestenje> Obavestenja { get; set; }
        public ICollection<Akcija> Akcije { get; set; }
        [BindProperty]
        public Obavestenje Obavestenje { get; set; }

        public ICollection<Skaut> Skauti { get; set; }
        public ICollection<Organizator> Organizatori { get; set; }

        public string SearchString { get; set; }
        //[BindProperty, Required]
        public FilterOptions SelectedFilter { get; set; }
        #endregion

        #region ViewModel
        public class ViewModel
        {
            public int BrojAkcija { get; set; }
            public int SakupljenoSmeca { get; set; }
            public int UcestvovaloCistaca { get; set; }
        }

        public ViewModel viewModel;
        #endregion

        public async Task OnGet(ViewModel vm, string searchString, FilterOptions filter)
        {
            this.Obavestenja = await _db.Obavestenja.OrderByDescending(x => x.DatumKreiranja).ToListAsync();
            this.SelectedFilter = filter;

            viewModel = vm;

            viewModel.BrojAkcija = _db.Akcijas.Count();
            viewModel.UcestvovaloCistaca = _db.Ucestvuje.Select(u => u.SkautID).Distinct().Count();
            viewModel.SakupljenoSmeca = _db.Skauts.Sum(s => s.SakupljenoSmeca);

            if (HttpContext.User.IsInRole("Skaut"))
            {
                this.Skaut = _db.Skauts.Where(s => s.Username == HttpContext.User.Identity.Name).FirstOrDefault();
                this.Akcije = this.GetAkcijeUKojimaUcestvujeUlogovaniSkaut(Skaut.SkautID);
            }
            else if (HttpContext.User.IsInRole("Organizator"))
            {
                this.Organizator = _db.Organizators.Where(org => org.Username == HttpContext.User.Identity.Name).FirstOrDefault();
                this.Akcije = this.GetAkcijeKojeJeOrganizovaoOrganizator(Organizator.OrganizatorID);
            }
            else
            {
                this.Administrator = _db.Administrators.Where(adm => adm.Username == HttpContext.User.Identity.Name).FirstOrDefault();
                SearchString = searchString;

                //REFAKTORISANO
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    //IQueryable<Skaut> skauts = from s in _db.Skauts
                //    //                           select s;

                //    //IQueryable<Organizator> organizators = from o in _db.Organizators
                //    //                                       select o;

                //    List<string> searchTerms = searchString.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                //    if (searchTerms.Count == 3)
                //    {
                //        Skauti = _db.Skauts.Where(p =>
                //            (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                //            (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper())) &&
                //            (p.Username.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[2].ToUpper()))
                //            ).ToList();

                //        Organizatori = _db.Organizators.Where(p =>
                //        (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                //        (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper())) &&
                //        (p.Username.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[2].ToUpper()))
                //        ).ToList();
                //    }
                //    else if (searchTerms.Count == 2)
                //    {
                //        Skauti = _db.Skauts.Where(p =>
                //        (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                //        (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper()))
                //        ).ToList();

                //        Organizatori = _db.Organizators.Where(p =>
                //        (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                //        (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper()))
                //        ).ToList();
                //    }
                //    else if (searchTerms.Count == 1)
                //    {
                //        Skauti = _db.Skauts.Where(p =>
                //        p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())
                //        ).ToList();

                //        Organizatori = _db.Organizators.Where(p =>
                //        p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())
                //        ).ToList();
                //    }
                //}
                //else
                //{
                //    Skauti = GetAllSkauts();
                //    Organizatori = GetAllOrganizators();
                //}
                switch (filter)
                {
                    case FilterOptions.Svi:
                        {
                            Skauti = GetSkautiBasedOnSearchString(searchString);
                            Organizatori = GetOrganizatoriBasedOnSearchString(searchString);
                            break;
                        };
                    case FilterOptions.Skaut:
                        {
                            Skauti = GetSkautiBasedOnSearchString(searchString);
                            Organizatori = null;
                            break;
                        }
                    case FilterOptions.Organizator:
                        {
                            Organizatori = GetOrganizatoriBasedOnSearchString(searchString);
                            Skauti = null;
                            break;
                        }
                    default:
                        {
                            Skauti = GetAllSkauts();
                            Organizatori = GetAllOrganizators();
                            break;
                        }
                }
            }
        }


        #region GET
        private IList<Akcija> GetAkcijeUKojimaUcestvujeUlogovaniSkaut(int skautId)
        {
            var ucestvuje = _db.Ucestvuje.Where(u => u.SkautID == skautId).ToList();
            IList<Akcija> akcijeUKojimUcestvujeSkaut = new List<Akcija>();
            foreach (Ucestvuje u in ucestvuje)
            {
                akcijeUKojimUcestvujeSkaut.Add(u.Akcija);
            }

            return akcijeUKojimUcestvujeSkaut;
        }

        private IList<Akcija> GetAkcijeKojeJeOrganizovaoOrganizator(int organizatorId)
        {
            var akcijeKojeJeOrganizovaoSkaut = _db.OrgNaLok.Where(o => o.OrganizatorID == organizatorId).Select(o => o.Akcija).ToList();
            return akcijeKojeJeOrganizovaoSkaut;
        }
        #endregion

        #region POST
        #region Organizator
        public async Task<IActionResult> OnPostDodajObavestenje()
        {
            if (Obavestenje.Sadrzaj != null)
            {
                Obavestenje.DatumKreiranja = DateTime.Now;
                Obavestenje.OrganizatorID = _db.Organizators
                    .Where(u => u.Username == HttpContext.User.Identity.Name)
                    .Select(p => p.OrganizatorID).FirstOrDefault();
                await _db.AddAsync(Obavestenje);
                await _db.SaveChangesAsync();
            }
            return LocalRedirect("/Pocetna");
        }
        #endregion

        #region Administrator
        public async Task<IActionResult> OnPostDeleteObavestenje(int id)
        {
            var obavestenje = await _db.Obavestenja.FindAsync(id);

            if (obavestenje == null)
            {
                return NotFound();
            }

            _db.Obavestenja.Remove(obavestenje);
            await _db.SaveChangesAsync();

            return LocalRedirect("/Pocetna");
        }

        public async Task<IActionResult> OnPostDeleteSkaut(int id)
        {
            var skaut = await _db.Skauts.FindAsync(id);

            if (skaut == null)
            {
                return NotFound();
            }

            _db.Skauts.Remove(skaut);
            await _db.SaveChangesAsync();

            return Redirect("/Pocetna");
        }

        public async Task<IActionResult> OnPostDeleteOrganizator(int id)
        {
            var organizator = await _db.Organizators.FindAsync(id);

            if (organizator == null)
            {
                return NotFound();
            }

            _db.Organizators.Remove(organizator);
            await _db.SaveChangesAsync();

            return Redirect("/Pocetna");
        }

        public async Task<IActionResult> OnPostPromoteSkauta(int id)
        {
            var skautZaUnapredjenje = await _db.Skauts.FindAsync(id);

            if (skautZaUnapredjenje == null)
            {
                return NotFound();
            }

            var noviOrganizator = new Organizator
            {
                Ime = skautZaUnapredjenje.Ime,
                Prezime = skautZaUnapredjenje.Prezime,
                Username = skautZaUnapredjenje.Username,
                Password = skautZaUnapredjenje.Password,
                EMail = skautZaUnapredjenje.EMail,
                ImagePath = skautZaUnapredjenje.ImagePath,
                DatumDodavanja = DateTime.Now,
                AdministratorID = _db.Administrators.Where(a => a.Username == HttpContext.User.Identity.Name).
                Select(a => a.AdministratorID).FirstOrDefault()
            };

            _db.Organizators.Add(noviOrganizator);
            _db.Skauts.Remove(skautZaUnapredjenje);
            await _db.SaveChangesAsync();

            return Redirect("/Pocetna");
        }
        #endregion

        
        private ICollection<Organizator> GetAllOrganizators()
        {
            return _db.Organizators.ToList();
        }

        private ICollection<Skaut> GetAllSkauts()
        {
            return _db.Skauts.ToList();
        }

        private Skaut GetByUsernameSkaut(string searchedUsername)
        {
            Skaut searchedSkaut = _db.Skauts.Where(s => s.Username == searchedUsername).FirstOrDefault();

            return searchedSkaut;
        }

        private Organizator GetByUsernameOrganizator(string searchedUsername)
        {
            Organizator searchedOrganizator = _db.Organizators.Where(s => s.Username == searchedUsername).FirstOrDefault();

            return searchedOrganizator;
        }

        private ICollection<Organizator> GetOrganizatoriBasedOnSearchString(string searchString)
        {
            if(searchString != null)
            { 
            List<string> searchTerms = searchString.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                switch (searchTerms.Count())
                {
                    case 3:
                        {
                            ICollection<Organizator> organizatori = _db.Organizators.Where(p =>
                   (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                   (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper())) &&
                   (p.Username.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[2].ToUpper()))
                   ).ToList();

                            return organizatori;
                        }
                    case 2:
                        {
                            ICollection<Organizator> organizatori = _db.Organizators.Where(p =>
    (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
    (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper()))
    ).ToList();

                            return organizatori;
                        }
                    case 1:
                        {
                            ICollection<Organizator> organizatori = _db.Organizators.Where(p =>
                    p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())
                    ).ToList();

                            return organizatori;
                        }
                    default:
                        {
                            return GetAllOrganizators();
                        }
                }
            }
            else
            {
                return GetAllOrganizators();
            }
        }

        private ICollection<Skaut> GetSkautiBasedOnSearchString(string searchString)
        {
            if (searchString != null)
            {
                List<string> searchTerms = searchString.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                switch (searchTerms.Count())
                {
                    case 3:
                        {
                            ICollection<Skaut> skauti = _db.Skauts.Where(p =>
        (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
        (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper())) &&
        (p.Username.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[2].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[2].ToUpper()))
        ).ToList();

                            return skauti;
                        }
                    case 2:
                        {
                            ICollection<Skaut> skauti = _db.Skauts.Where(p =>
                    (p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())) &&
                    (p.Username.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[1].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[1].ToUpper()))
                    ).ToList();

                            return skauti;
                        }
                    case 1:
                        {
                            ICollection<Skaut> skauti = _db.Skauts.Where(p =>
                    p.Username.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Ime.ToUpper().Contains(searchTerms[0].ToUpper()) || p.Prezime.ToUpper().Contains(searchTerms[0].ToUpper())
                    ).ToList();

                            return skauti;
                        }
                    default:
                        {
                            return GetAllSkauts();
                        }
                }
            }
            else
            {
                return GetAllSkauts();
            }
        }
    }
    #endregion

}
