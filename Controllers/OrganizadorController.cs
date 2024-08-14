using SportSync.Data;
using SportSync.Models;
using SportSync.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SportSync.Controllers
{
    [Authorize(Roles = "Organizador, Administrador")]// Esto requiere autenticación para todas las acciones en este controlador
    public class OrganizadorController : Controller
    {
        private readonly SportSyncContext _context;

        public OrganizadorController(SportSyncContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult agregarEquipos()
        {
           return View("~/Views/Organizador/torneos/agregarEquipos.cshtml");
        }
        
        public IActionResult gestionar()
        {
            return View("~/Views/Organizador/torneos/gestionar.cshtml");
        }
        
        public IActionResult gestionarTorneo()
        {
            return View("~/Views/Organizador/torneos/gestionarTorneo.cshtml");
        }
        [HttpGet]
        public IActionResult crearTorneo()
        {
            
            return View("~/Views/Organizador/torneos/crearTorneo.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTorneo(Torneo model, IFormFile ImgTorneoFile)
        {
            if (ModelState.IsValid)
            {
                // Manejar la carga de la imagen
                if (ImgTorneoFile != null && ImgTorneoFile.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var filePath = Path.Combine(uploads, Path.GetFileName(ImgTorneoFile.FileName));

                    // Guardar la imagen en el servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImgTorneoFile.CopyToAsync(fileStream);
                    }

                    // Guardar la ruta de la imagen en el modelo
                    model.ImgTorneo = $"/images/{Path.GetFileName(ImgTorneoFile.FileName)}";
                }

                _context.Torneos.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult reglas()
        {
            return View("~/Views/Organizador/torneos/reglas.cshtml");
        }
        
        public IActionResult coaches()
        {
            return View();
        }
        
        public IActionResult arbitros()
        {
            return View();
        }
        
        public IActionResult contabilidad()
        {
            return View();
        }
        
        public IActionResult contadores()
        {
             return View("~/Views/Organizador/contabilidad/contadores.cshtml");
        }
        
        public IActionResult cuenta()
        {
             return View("~/Views/Organizador/contabilidad/cuenta.cshtml");
        }
        
        public IActionResult equipos()
        {
            return View();
        }
        
        public IActionResult gestionarEquipo()
        {
             return View("~/Views/Organizador/equipos/gestionarEquipo.cshtml");
        }
        
        public IActionResult noticias()
        {
            return View();
        }
    }
}
