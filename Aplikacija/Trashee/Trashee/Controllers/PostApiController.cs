using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using Trashee.Models;

namespace Trashee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PostApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        class SearchTerms
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Username { get; set; }
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                 List<string> terms = new List<string>(term.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries));
                if (terms.Count == 3)
                {
                    var skauti = _db.Skauts.Where(p =>
                        (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                        (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1])) &&
                        (p.Username.Contains(terms[2]) || p.Ime.Contains(terms[2]) || p.Prezime.Contains(terms[2]))
                        )
                        .Select(p => new
                        {
                            p.Ime,
                            p.Prezime,
                            p.Username
                        }).ToList();

                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }
                else if (terms.Count == 2)
                {
                    var skauti = _db.Skauts.Where(p =>
                    (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                    (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1]))
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username
                    }).ToList();
                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }
                else if (terms.Count == 1)
                {
                    var skauti = _db.Skauts.Where(p =>
                    p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username
                    }).ToList();
                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("searchadminview")]
        public async Task<IActionResult> SearchAdminView()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                List<string> terms = new List<string>(term.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries));
                if (terms.Count == 3)
                {
                    var skauti = _db.Skauts.Where(p =>
                        (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                        (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1])) &&
                        (p.Username.Contains(terms[2]) || p.Ime.Contains(terms[2]) || p.Prezime.Contains(terms[2]))
                        )
                        .Select(p => new
                        {
                            p.Ime,
                            p.Prezime,
                            p.Username
                        }).ToList();

                    var organizatori = _db.Organizators.Where(p =>
                    (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                    (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1])) &&
                    (p.Username.Contains(terms[2]) || p.Ime.Contains(terms[2]) || p.Prezime.Contains(terms[2]))
                    )
                        .Select(p => new
                        {
                            p.Ime,
                            p.Prezime,
                            p.Username
                        }).ToList();

                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Tip: Skaut-cistac Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    foreach (var s in organizatori)
                    {
                        listaZaPrikazivanje.Add($"Tip: Organizator Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }
                else if (terms.Count == 2)
                {
                    var skauti = _db.Skauts.Where(p =>
                    (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                    (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1]))
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username
                    }).ToList();

                    var organizatori = _db.Organizators.Where(p =>
                    (p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])) &&
                    (p.Username.Contains(terms[1]) || p.Ime.Contains(terms[1]) || p.Prezime.Contains(terms[1]))
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username
                    }).ToList();


                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Tip: Skaut-cistac Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    foreach (var s in organizatori)
                    {
                        listaZaPrikazivanje.Add($"Tip: Organizator Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }
                else if (terms.Count == 1)
                {
                    var skauti = _db.Skauts.Where(p =>
                    p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username,
                    }).ToList();

                    var organizatori = _db.Organizators.Where(p =>
                    p.Username.Contains(terms[0]) || p.Ime.Contains(terms[0]) || p.Prezime.Contains(terms[0])
                    )
                    .Select(p => new
                    {
                        p.Ime,
                        p.Prezime,
                        p.Username,
                    }).ToList();

                    var listaZaPrikazivanje = new List<string>();
                    foreach (var s in skauti)
                    {
                        listaZaPrikazivanje.Add($"Tip: Skaut-cistac Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    foreach (var s in organizatori)
                    {
                        listaZaPrikazivanje.Add($"Tip: Organizator Ime: {s.Ime} Prezime: {s.Prezime} Username: {s.Username}");
                    }
                    return Ok(listaZaPrikazivanje);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
