using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Trashee.Extensions;
using Trashee.Models;

namespace Trashee.Pages
{
    public class LoginModel : PageModel
    {
        #region DI
        private readonly ApplicationDbContext _db;

        
        //public Skaut Skaut { get; set; }
        //public Organizator Organizator { get; set; }
        //public Administrator Administrator { get; set; }
        #endregion


        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            #region snippet2
            //await HttpContext.SignOutAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion
            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUlr = null) //cookie se kreira ovde
        {

            ReturnUrl = returnUlr;
            if (ModelState.IsValid)
            {
                // Use Input.Email and Input.Password to authenticate the user
                // with your custom authentication logic.
                //
                // For demonstration purposes, the sample validates the user
                // on the email address maria.rodriguez@contoso.com with 
                // any password that passes model validation.

                //var user = await AuthenticateSkaut(Input.Username, Input.Password);

                //var user = await AuthenticateUser(Input.Username, Input.Password);


                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return Page();
                //}

                List<Claim> claims;

                if (AuthenticateSkaut(Input.Username, Input.Password) == true)
                {
                    claims = SetClaim("Skaut");
                }
                else if (AuthenticateOrganizator(Input.Username, Input.Password) == true)
                {
                    claims = SetClaim("Organizator");
                }
                else if (AuthenticateAdministrator(Input.Username, Input.Password) == true)
                {
                    claims = SetClaim("Administrator");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                #region snippet1
                //var claims = new List<Claim>
                //{
                //    //new Claim(ClaimTypes.Name, user.Email),
                //   //new Claim("FullName", user.FullName),
                //   // new Claim(ClaimTypes.Role, "Administrator"),
                //   //new Claim("ID", user.ID),
                //   new Claim(ClaimTypes.Role,"Skaut"),
                //};

                //var claims = SetClaim(user, "Skaut");

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                #endregion

                _logger.LogInformation($"User {Input.Username} logged in at {DateTime.UtcNow}.",
                    Input.Username, DateTime.UtcNow);

                return LocalRedirect(Url.GetLocalUrl(returnUlr));
            }

            // Something failed. Redisplay the form.
            return Page();

           // return LocalRedirect("/Login");
        }
        //private async Task<Skaut> AuthenticateSkaut(string username, string password)
        //{
        //    await Task.Delay(500);
        //    Skaut = _db.Skauts.Where(b => b.Username == username).FirstOrDefault();
        //    if (Skaut.Username == username && Skaut.Password == password)
        //        return Skaut;
        //    return null;
        //}

        private bool AuthenticateSkaut(string username, string password)
        {
            Skaut skaut =  _db.Skauts.Where(b => b.Username == username).FirstOrDefault();
            if(skaut == null)
            {
                // return null;
                return false;

            }
            if (skaut.Username == username && skaut.Password == password)
                // return skaut;
                return true;
            // return null;
            return false;
        }

        private bool AuthenticateOrganizator(string username, string password)
        {
            Organizator organizator =  _db.Organizators.Where(b => b.Username == username).FirstOrDefault();
            if (organizator == null)
            {
                // return null;
                return false;

            }
            if (organizator.Username == username && organizator.Password == password)
                // return skaut;
                return true;
            // return null;
            return false;
        }

        private bool AuthenticateAdministrator(string username, string password)
        {
            Administrator administrator = _db.Administrators.Where(b => b.Username == username).FirstOrDefault();
            if (administrator == null)
            {
                // return null;
                return false;

            }
            if (administrator.Username == username && administrator.Password == password)
                // return skaut;
                return true;
            // return null;
            return false;
        }

        private List<Claim> SetClaim(string role)
        {
            List<Claim> claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, user.Email),
                   //new Claim("FullName", user.FullName),
                   // new Claim(ClaimTypes.Role, "Administrator"),
                   //new Claim("ID", user.ID),
                   new Claim(ClaimTypes.Name, Input.Username),
                   new Claim(ClaimTypes.Role, role),
                };
            return claims;
        }

    }
}