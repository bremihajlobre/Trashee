using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Trashee.Models;

namespace Trashee.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Skaut Skaut { get; set; }
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnPostRegister()
        {
            if (ModelState.IsValid)
            {
                Skaut.DateReg = DateTime.Now;
                await _db.Skauts.AddAsync(Skaut);
                await _db.SaveChangesAsync();
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}
