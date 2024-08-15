using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SportSync.Models;
using SportSync.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportSync.Data;
using SportSync.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportSync.Controllers
{
    public class HomeController : Controller
    {
        private readonly SportsyncContext _context;

 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SportsyncContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                return RedirectBasedOnRole(userRole);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Username && u.Pass == model.Pass);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre + " " + usuario.ApPaterno + " " + usuario.ApMaterno),
                        new Claim(ClaimTypes.Role, GetRoleName(usuario.IdRol))
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true
                        });

                    return RedirectBasedOnRole(GetRoleName(usuario.IdRol));
                }

                ModelState.AddModelError(string.Empty, "Credenciales no válidas.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }


        private string GetRoleName(int idRol)
        {
            return idRol switch
            {
                1 => "Administrador",
                2 => "Organizador",
                3 => "Coach",
                4 => "Árbitro",
                5 => "Contador",
                6 => "Medico",
                _ => "Usuario"
            };
        }

        private IActionResult RedirectBasedOnRole(string role)
        {
            return role switch
            {
                "Organizador" => RedirectToAction("Index", "Organizador"),
                "Administrador" => RedirectToAction("Index", "Administrador"),
                "Coach" => RedirectToAction("Index", "Coach"),
                // Añadir más roles según sea necesario
                _ => RedirectToAction("Index", "Home")
            };
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}