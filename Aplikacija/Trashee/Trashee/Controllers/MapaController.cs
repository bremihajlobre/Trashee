using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Trashee.Models;

namespace Trashee.Controllers
{
    [Route("[controller]/[action]")]
    public class MapaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MapaController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetAllLokacije")]
        public JsonResult GetAllLokacije()
        {
            ICollection<Lokacija> lokacije = _db.Lokacijas
                .ToList();

            return new JsonResult(lokacije);
        }

        [HttpPost]
        public void DodajLokaciju(double lat, double lng, List<IFormFile> lokacijaImages)
        {
            Lokacija lokacija = new Lokacija
            {
                Latitude = (float)lat,
                Longitude = (float)lng,
                DatumPrijavljivanja = DateTime.Now,
                SlikeLokacija = lokacijaImages.Select(image => new SlikeLokacija
                {
                    Slika = UploadImage(image)
                }).ToList(),
                SkautID = _db.Skauts.Where(s => s.Username == HttpContext.User.Identity.Name)
                .Select(s => s.SkautID)
                .FirstOrDefault()
            };

            _db.Lokacijas.Add(lokacija);
            _db.SaveChanges();
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
    }
}
