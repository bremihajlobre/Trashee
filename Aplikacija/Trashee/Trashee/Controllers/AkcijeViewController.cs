using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Trashee.Models;
using static Trashee.Pages.AkcijaModel;

namespace Trashee.Controllers
{
    [Route("[controller]")]
    public class AkcijeViewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AkcijeViewController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Details")]
        public ActionResult Details(int id)
        {
            
            Akcija akcija = _db.Akcijas.Where(a => a.AkcijaID == id).FirstOrDefault();
            var status = Enum.Parse<FilterOptions>(akcija.Status);

            var viewName = $"ViewAkcija/{ GetViewName(status)}";

            return new PartialViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "Akcija", akcija } }
            };
        }

        [HttpPost]
        [Route("DetailsLokacija")]
        public ActionResult DetailsLokacija(int id)
        {

            Lokacija lokacija = _db.Lokacijas.Where(a => a.LokacijaID == id).FirstOrDefault();
            var status = Enum.Parse<FilterOptions>("Lokacije");

            var viewName = $"ViewAkcija/{ GetViewName(status)}";

            return new PartialViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "Lokacija", lokacija } }
            };
        }

        public Akcija Akcija { get; set; }

        [HttpPost]
        [Route("NapraviAkciju")]
        public JsonResult NapraviAkciju(int brojPotrebnihCistaca, int lokacijaId, DateTime datumOdrzavanja)
        {
            this.Akcija = GetAkcija(brojPotrebnihCistaca);
            var lokacija = GetLokacija(lokacijaId);
            if (brojPotrebnihCistaca != 0 && lokacijaId != 0 && datumOdrzavanja != DateTime.MinValue && datumOdrzavanja > lokacija.DatumPrijavljivanja)
            {
                OrgNaLok orgNaLok = new OrgNaLok
                {
                    Akcija = GetAkcija(brojPotrebnihCistaca),
                    Lokacija = lokacija,
                    Organizator = GetOrganizator(HttpContext.User.Identity.Name),
                    DatumOrganizovanja = datumOdrzavanja
                };

                _db.OrgNaLok.Add(orgNaLok);

                _db.SaveChanges();

                return new JsonResult(new { success = true, responseText = "Uspesno ste napravili akciju" });
            }
            else
            {
                return new JsonResult(new { success = false, responseText = "Dodavanja akcije je neuspesno, proverite unete podatke" });
            }
        }

        [HttpPost]
        [Route("DeleteLokacija")]
        public JsonResult DeleteLokacija(int lokacijaId)
        {
            
            var lokacija = GetLokacija(lokacijaId);

            if (lokacija != null)
            {
                foreach(var slika in lokacija.SlikeLokacija)
                {
                    _db.SlikeLokacija.Remove(slika);
                }
                _db.Lokacijas.Remove(lokacija);
                _db.SaveChanges();
                return new JsonResult(new { success = true, responseText = "Lokacija je uspesno obrisana" });
            }
            else
            {
                return new JsonResult(new { success = false, responseText = "Lokacija nije uspesno obrisana" });
            }
        }

        [HttpPost]
        [Route("UploadSlikePosle")]
        public JsonResult UploadSlikePosle(int akcijaId, List<IFormFile> lokacijaImages)
        {
            var akcija = _db.Akcijas
                .Where(a => a.AkcijaID == akcijaId).FirstOrDefault();

            akcija.Status = FilterOptions.Uspesna.ToString();
            akcija.SlikeAkcija = lokacijaImages.Select(image => new SlikeAkcija
            {
                Slika = UploadImage(image)
            }).ToList();

            _db.SaveChanges();

            return new JsonResult(new { success = true, responseText = "Akcija je uspesno zavrsena" });
        }

        private string UploadImage(IFormFile slika)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + slika.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                slika.CopyTo(fileStream);
            }
            return uniqueFileName;

        }

        public Organizator GetOrganizator(string username)
        {
            return _db.Organizators
                .Where(o => o.Username == username)
                .FirstOrDefault();
        }

        public Akcija GetAkcija(int brojPotrebnihCistaca)
        {
            return new Akcija
            {
                BrojUcesnika = brojPotrebnihCistaca,
                Status = "Predstojeca"
            };
        }

        public Lokacija GetLokacija(int lokacijaId)
        {
            return _db.Lokacijas.Find(lokacijaId);
        }

        private string GetViewName(FilterOptions status)
        {
            switch (status)
            {
                case FilterOptions.Cekanje:
                    return "Lokacije/_CekanjeLokacija";
                case FilterOptions.Uspesna:
                    return "Akcije/_Zavrsena";
                case FilterOptions.Predstojeca:
                    return "Akcije/_Predstojeca";
                case FilterOptions.Otkazana:
                    return "Akcije/_Otkazana";
                case FilterOptions.Lokacije:
                    return "Lokacije/_OrganizatorLokacija";
                default:
                    return "Lokacije/_OrganizatorLokacija";
            }
        }
    }
}
