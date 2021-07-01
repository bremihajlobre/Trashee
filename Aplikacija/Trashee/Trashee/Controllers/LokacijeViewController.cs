using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Trashee.Models;
using static Trashee.Pages.AkcijaModel;

namespace Trashee.Controllers
{
    [Route("[controller]")]
    public class LokacijeViewController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LokacijeViewController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("Details")]
        public ActionResult Details(int id)
        {

            Akcija akcija = _db.Akcijas.Where(a => a.AkcijaID == id).FirstOrDefault();
            var status = Enum.Parse<FilterOptions>(akcija.Status);

            var isAdmin = User.IsInRole("Administrator");

            var viewName = isAdmin
                ? "Lokacije/_AdministratorLokacija"
                : $"ViewAkcija/{ GetViewName(status)}";

            return new PartialViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "Akcija", akcija } }
            };
        }

        private string GetViewName(FilterOptions status)
        {
            switch (status)
            {
                case FilterOptions.Cekanje:
                    return "Lokacije/_CekanjeLokacija";
                case FilterOptions.Lokacije:
                    return "Lokacije/_OrganizatorLokacija";
                default:
                    return "Lokacije/_OrganizatorLokacija";
            }
        }
    }
}   