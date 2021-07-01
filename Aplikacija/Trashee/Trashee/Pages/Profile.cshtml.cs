using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trashee.Models;

namespace Trashee.Pages
{
    [Authorize]
    public class ProfilModel : PageModel
    {
        #region DI
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfilModel(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }
        #endregion

        #region Username
        private string username;

        public string GetUsername()
        {
            return username;
        }

        public void SetUsername()
        {
            username = HttpContext.User.Identity.Name;
        }

        #endregion

        #region Role
        private string role;
        public string GetRole()
        {
            return role;
        }
        public void SetRole()
        {
            role = ((ClaimsIdentity)HttpContext.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();
        }

        #endregion

        #region UserId
        private int userId;
        public int GetUserId()
        {
            SetUserId();
            return userId;
        }

        public void SetUserId()
        {
            SetUsername();
            SetRole();

            if (role == "Skaut")
                userId = _db.Skauts.Where(s => s.Username == this.GetUsername()).FirstOrDefault().SkautID;
            else if (role == "Organizator")
                userId = _db.Organizators.Where(s => s.Username == this.GetUsername()).FirstOrDefault().OrganizatorID;
            else
                userId = _db.Administrators.Where(s => s.Username == this.GetUsername()).FirstOrDefault().AdministratorID;

        }
        #endregion

        #region InputModel
        public class InputModel
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [StringLength(50, MinimumLength = 5)]
            public string Username { get; set; }
            public string Password { get; set; }
            [Compare("Password", ErrorMessage = "Vrednosti se ne poklapaju")]
            public string ConfirmPassword { get; set; }

            public DateTime DateReg { get; set; }

            public int Poeni { get; set; }

            public int SakupljenoSmeca { get; set; }
            public int UkupnoPrisutan { get; set; } = 0;

            public IFormFile ProfileImage { get; set; }

            public string PrijavljeniSkautPodaci { get; set; }

            public string ProfileImagePath { get; set; }

            public int PrijavioLokacije { get; set; }
            public int OrganizovaoUkupno { get; set; }

            public int BrojDanaClanstva { get; set; }
        }


        public InputModel Input = new InputModel();

        #endregion

        public void OnGet()
        {
            SetUsername();
            SetRole();
            if (role == "Skaut")
            {
                Skaut skaut = _db.Skauts.Where(s => s.Username == username).FirstOrDefault();
                if (skaut != null)
                {
                    Input.Ime = skaut.Ime;
                    Input.Prezime = skaut.Prezime;
                    Input.Email = skaut.EMail;
                    Input.Username = skaut.Username;
                    Input.DateReg = skaut.DateReg;
                    Input.Poeni = skaut.Poeni;
                    Input.SakupljenoSmeca = skaut.SakupljenoSmeca;
                    Input.ProfileImagePath = skaut.ImagePath;
                    Input.UkupnoPrisutan = skaut.Ucestvuje.Count();
                    Input.PrijavioLokacije = skaut.PrijavljeneLokacije.Count();
                    Input.BrojDanaClanstva = IzracunaBrojDanaClanstva(skaut.DateReg); 
                }

            }
            else if (role == "Organizator")
            {
                Organizator organizator = _db.Organizators.Where(s => s.Username == username).FirstOrDefault();
                if (organizator != null)
                {
                    Input.Ime = organizator.Ime;
                    Input.Prezime = organizator.Prezime;
                    Input.Email = organizator.EMail;
                    Input.Username = organizator.Username;
                    Input.DateReg = organizator.DatumDodavanja;
                    Input.ProfileImagePath = organizator.ImagePath;
                    Input.OrganizovaoUkupno = organizator.OrganizovaneAkcije.Count();
                    Input.BrojDanaClanstva = IzracunaBrojDanaClanstva(organizator.DatumDodavanja);
                }
            }
            else if (role == "Administrator")
            {
                Administrator administrator = _db.Administrators.Where(s => s.Username == username).FirstOrDefault();
                if (administrator != null)
                {
                    Input.Ime = administrator.Ime;
                    Input.Prezime = administrator.Prezime;
                    Input.Email = administrator.EMail;
                    Input.Username = administrator.Username;
                    Input.ProfileImagePath = administrator.ImagePath;
                }
            }
        }


        public async Task<IActionResult> OnPostEdit(InputModel Input)
        {
            SetRole();
            SetUsername();
            SetUserId();
            if (ModelState.IsValid)
            {
                if (role == "Skaut")
                {
                    var skautFromDb = await _db.Skauts.FindAsync(GetUserId());

                    if (skautFromDb != null)
                    {
                        skautFromDb.Ime = Input.Ime;
                        skautFromDb.Prezime = Input.Prezime;
                        skautFromDb.EMail = Input.Email;
                        skautFromDb.Username = Input.Username;
                        if (Input.Password == null)
                        {
                            Input.Password = skautFromDb.Password;
                        }
                        else
                        {
                            skautFromDb.Password = Input.Password;
                        }
                    }
                }
                else if (role == "Organizator")
                {
                    var organizatorFromDb = await _db.Organizators.FindAsync(GetUserId());

                    if (organizatorFromDb != null)
                    {
                        organizatorFromDb.Ime = Input.Ime;
                        organizatorFromDb.Prezime = Input.Prezime;
                        organizatorFromDb.EMail = Input.Email;
                        organizatorFromDb.Username = Input.Username;
                        if (Input.Password == null)
                        {
                            Input.Password = organizatorFromDb.Password;
                        }
                        else
                        {
                            organizatorFromDb.Password = Input.Password;
                        }
                    }
                }
                else if (role == "Administrator")
                {
                    var administratorFromDb = await _db.Administrators.FindAsync(GetUserId());

                    if (administratorFromDb != null)
                    {
                        administratorFromDb.Ime = Input.Ime;
                        administratorFromDb.Prezime = Input.Prezime;
                        administratorFromDb.EMail = Input.Email;
                        administratorFromDb.Username = Input.Username;
                        if (Input.Password == null)
                        {
                            Input.Password = administratorFromDb.Password;
                        }
                        else
                        {
                            administratorFromDb.Password = Input.Password;
                        }
                    }
                }
                await _db.SaveChangesAsync();
                return LocalRedirect("/Profile");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUploadImage(InputModel Input)
        {
            SetRole();
            SetUserId();
            if (role == "Skaut")
            {
                var skautFromDb = await _db.Skauts.FindAsync(GetUserId());

                skautFromDb.ImagePath = uploadImage(skautFromDb.ImagePath, Input);
            }
            else if (role == "Organizator")
            {
                var organizatorFromDb = await _db.Organizators.FindAsync(GetUserId());

                if (organizatorFromDb != null)
                {
                    organizatorFromDb.ImagePath = uploadImage(organizatorFromDb.ImagePath, Input);
                }
            }
            else if (role == "Administrator")
            {
                var administratorFromDb = await _db.Administrators.FindAsync(GetUserId());

                if (administratorFromDb != null)
                {
                    administratorFromDb.ImagePath = uploadImage(administratorFromDb.ImagePath, Input);
                }
            }
            await _db.SaveChangesAsync();
            return LocalRedirect("/Profile");
        }

        private string uploadImage(string imagePathFromDb, InputModel Input)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.ProfileImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (imagePathFromDb != null)
            {
                string deleteFilePath = Path.Combine(uploadsFolder, imagePathFromDb);
                if (System.IO.File.Exists(deleteFilePath))
                {
                    System.IO.File.Delete(deleteFilePath);
                }
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Input.ProfileImage.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        #region Zalba

        [BindProperty]
        public Zalba Zalba { get; set; }
        public async Task<IActionResult> OnPostZalba(InputModel input)
        {
            this.Zalba.SkautID = GetUserId();
            string prijavljeniSkautUsername = input.PrijavljeniSkautPodaci.Split(" ").Last<string>();

            Skaut prijavljeniSkaut = _db.Skauts.Where(p => p.Username == prijavljeniSkautUsername)
                .FirstOrDefault();

            if(prijavljeniSkaut == null)
            {
                return NotFound();
            }    

            Zalba.PrijavljeniUsername = prijavljeniSkaut.Username;

            Zalba.PrijavljeniSkaut = prijavljeniSkaut;

            if (ModelState.IsValid)
            {
                await _db.Zalbas.AddAsync(Zalba);
                await _db.SaveChangesAsync();
                return LocalRedirect("/Profile");
            }
            return LocalRedirect("/Profile");
        }
        #endregion



        private int IzracunaBrojDanaClanstva(DateTime date)
        {
            return (DateTime.Now - date).Days;
        }
    }
}