using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Trashee.Models;

namespace Trashee.Pages
{
    [Authorize]
    public class AkcijaModel : PageModel
    {
        #region DI
        private readonly ApplicationDbContext _db;
        public AkcijaModel(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Properties
        public ICollection<Akcija> Akcije { get; set; }

        public FilterOptions SelectedFilter { get; set; }
        
        public ICollection<int> AkcijeNaKojimaJeSkautPrijavljen { get; set; }

        public ICollection<Lokacija> Lokacije { get; set; }

        [BindProperty]
        public Akcija Akcija { get; set; }
        [BindProperty]
        public DateTime DatumOdrzavanja { get; set; }
        public enum FilterOptions
        {
            Sve,
            Uspesna,
            Predstojeca,
            Otkazana,
            Cekanje,
            Lokacije
        }
        #endregion

        #region GET
        public async Task<IActionResult> OnGet(FilterOptions filter)
        {
            if (User.IsInRole("Skaut"))
            {
                this.AkcijeNaKojimaJeSkautPrijavljen = await GetAkcijeNaKojimaJeSkautPrijavljen(User.Identity.Name);
            }
            this.SelectedFilter = filter;
            switch (filter)
            {
                case FilterOptions.Sve:
                    {
                        Akcije = await _db.Akcijas.ToListAsync();
                        SetStatusToAkcije();
                        Lokacije = GetLokacijeKojeNemajuAkciju();
                        break;
                    }
                case FilterOptions.Uspesna:
                    {
                        Akcije = await _db.Akcijas.Where(a => a.Status == filter.ToString()).ToListAsync();
                        break;
                    }
                case FilterOptions.Predstojeca:
                    {
                        Akcije = await _db.Akcijas.Where(a => a.Status == filter.ToString()).ToListAsync();
                        break;
                    }
                case FilterOptions.Otkazana:
                    {
                        Akcije = await _db.Akcijas.Where(a => a.Status == filter.ToString()).ToListAsync();
                        break;
                    }
                case FilterOptions.Cekanje:
                    {
                        Akcije = await _db.Akcijas.Where(a => a.Status == filter.ToString()).ToListAsync();
                        break;
                    }
                case FilterOptions.Lokacije:
                    {
                        Lokacije = GetLokacijeKojeNemajuAkciju();
                        break;
                    }
            }

            return Page();
        }

        public async Task<ICollection<int>> GetAkcijeNaKojimaJeSkautPrijavljen(string username)
        {
            var skautId = await GetSkautIdByUsername(username);

            var akcije = await _db.Ucestvuje
                .Where(u => u.SkautID == skautId)
                .Select(u => u.AkcijaID)
                .ToListAsync();

            return akcije;
        }
        #endregion


        public async Task<int> GetSkautIdByUsername(string username)
        {
            var skautId = await _db.Skauts.Where(s => s.Username == username)
                .Select(s => s.SkautID)
                .FirstOrDefaultAsync();
            return skautId;
        }

        public async Task<Skaut> GetSkautByUsername(string username)
        {
            var skaut = await _db.Skauts
                .Where(s => s.Username == username)
                .FirstOrDefaultAsync();

            return skaut;
        }

        public async Task<Akcija> GetAkcijaById(int akcijaId)
        {
            return await _db.Akcijas
                .FindAsync(akcijaId);
        }

        public async Task<Ucestvuje> GetUcestvuje(int akcijaId, int skautId)
        {
            var ucestvuje = await _db.Ucestvuje
                .FindAsync(akcijaId, skautId);

            return ucestvuje;
        }

        #region POST
        public async Task<IActionResult> OnPostPrijaviSkautaNaAkciju(string skautUsername, int akcijaId)
        
        {
            var skaut = await GetSkautByUsername(skautUsername);

            var akcija = await GetAkcijaById(akcijaId);
            akcija.BrojUcesnika++;

            var ucestvuje = new Ucestvuje
            {
                Skaut = skaut,
                Akcija = akcija
            };

            await _db.Ucestvuje.AddAsync(ucestvuje);
            await _db.SaveChangesAsync();

            return LocalRedirect("/Akcije");
        }

        public async Task<IActionResult> OnPostOdjaviSkautaSaAkcije(string skautUsername, int akcijaId)
        {
            var skaut = await GetSkautByUsername(skautUsername);
            var akcija = await GetAkcijaById(akcijaId);
            akcija.BrojUcesnika--;

            var ucestvuje = await GetUcestvuje(skaut.SkautID, akcija.AkcijaID);
            

            if (ucestvuje != null)
            {
                _db.Remove(ucestvuje);
                await _db.SaveChangesAsync();
            }
            return LocalRedirect("/Akcije");
        }

        public async Task<IActionResult> OnPostOtkaziAkciju(int id)
        {
            var akcija = await GetAkcijaById(id);

            akcija.Status = FilterOptions.Otkazana.ToString();
            await _db.SaveChangesAsync();

            return LocalRedirect("/Akcije");
        }

        public async void SetStatusToAkcije()
        {
            foreach(var akcija in this.Akcije)
            {
                if(CalculateTimeSpan(akcija.OrgNaLok.DatumOrganizovanja) > 2 && akcija.Status == FilterOptions.Predstojeca.ToString())
                {
                    akcija.Status = FilterOptions.Cekanje.ToString();
                }
            }
            await _db.SaveChangesAsync();
        }


        private int CalculateTimeSpan(DateTime vremeOrg)
        {
            TimeSpan timeSpan = DateTime.Now - vremeOrg;
            return timeSpan.Hours;
        }
        
        #endregion

        #region DELETE
        public async Task<IActionResult> OnPostDeleteAkciju(int id)
        {
            var akcija = await GetAkcijaById(id);

            if (akcija != null)
            {
                _db.Remove(akcija);
                await _db.SaveChangesAsync();
            }

            return LocalRedirect("/Akcije");
        }

        public Organizator GetOrganizator(string username)
        {
            return _db.Organizators
                .Where(o => o.Username == username)
                .FirstOrDefault();
        }

        public ICollection<Lokacija> GetLokacijeKojeNemajuAkciju()
        {
            return _db.Lokacijas
                .Where(l => l.OrgNaLok == null)
                .ToList();
        }
        #endregion
    }
}