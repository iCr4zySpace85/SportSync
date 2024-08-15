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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportSync.Models;
using SportSync.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportSync.Controllers
{
    [Authorize(Roles = "Organizador, Administrador")]
    public class OrganizadorController : Controller
    {
        private readonly SportsyncContext _context;

        public OrganizadorController(SportsyncContext context)
        {
            _context = context;
        }

       public IActionResult Index()
        {
            var torneos =  _context.Torneos.ToList();
            var deportes = new Dictionary<int, string>
            {
                { 1, "Fútbol" },
                { 2, "Basquetbol" },
                { 3, "Voleibol" }
            };
            var model = new TorneosViewModel
            {
                Torneos = torneos,
                Deportes = deportes
            };
            return View(model);
        }

        public IActionResult AgregarEquipos()
        {
            return View();
        }

        public IActionResult Gestionar()
        {
            return View();
        }

        // Acción para cargar los datos del torneo
       public IActionResult GestionarTorneo(int id)
        {
            var torneo = _context.Torneos
                .Include(t => t.EquiposTorneos)
                .ThenInclude(et => et.IdEquipoNavigation)
                .FirstOrDefault(t => t.IdTorneo == id);

            if (torneo == null)
            {
                return NotFound();
            }

            return View(torneo);
        }


        // Acción para guardar los cambios del torneo
        [HttpPost]
        public IActionResult GuardarCambios(Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(torneo);
                _context.SaveChanges();
                return RedirectToAction("GestionarTorneo", new { idTorneo = torneo.IdTorneo });
            }

            return View("GestionarTorneo", torneo);
        }


        // Acción para agregar un nuevo equipo
        // Acción para agregar un nuevo equipo
        [HttpPost]
        public async Task<IActionResult> AgregarEquipo(int idTorneo, string nombreEquipo, string categoria, int idDeporte)
        {
            var equipo = new Equipo
            {
                Nombre = nombreEquipo,
                Categoria = categoria,
                IdDeporte = idDeporte
    };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            var equipoTorneo = new EquiposTorneo
            {
                IdTorneo = idTorneo,
                IdEquipo = equipo.IdEquipo
            };

            _context.EquiposTorneos.Add(equipoTorneo);
            await _context.SaveChangesAsync();

            return RedirectToAction("GestionarTorneo", new { idTorneo = idTorneo });
        }




        // Acción para eliminar un equipo
        public IActionResult EliminarEquipo(int id)
        {
            var equipoTorneo = _context.EquiposTorneos.FirstOrDefault(et => et.IdEquipoTorneo == id);

            if (equipoTorneo != null)
            {
                _context.EquiposTorneos.Remove(equipoTorneo);
                _context.SaveChanges();
                return RedirectToAction("GestionarTorneo", new { idTorneo = equipoTorneo.IdTorneo });
            }

            return NotFound(); // O redirigir a una página de error apropiada
        }



        [HttpGet]
        public IActionResult CrearTorneo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearTorneo(Torneo model, IFormFile ImgTorneoFile)
        {
            
                if (ImgTorneoFile != null && ImgTorneoFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(ImgTorneoFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImgTorneo", "Tipo de archivo no permitido. Por favor, sube una imagen.");
                        return View(model);
                    }

                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    var filePath = Path.Combine(uploads, Path.GetFileName(ImgTorneoFile.FileName));

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImgTorneoFile.CopyToAsync(fileStream);
                    }

                    model.ImgTorneo = $"/images/{Path.GetFileName(ImgTorneoFile.FileName)}";
                }
                else
                {
                    ModelState.AddModelError("ImgTorneo", "Por favor, sube una imagen.");
                    return View(model);
                }

                _context.Torneos.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            

        }


        public IActionResult Reglas()
        {
            return View("~/Views/Organizador/torneos/reglas.cshtml");
        }

        public IActionResult Coaches()
        {
            return View();
        }

        public IActionResult Arbitros()
        {
            return View();
        }

        public IActionResult Contabilidad()
        {
            return View();
        }

        public IActionResult Contadores()
        {
            return View("~/Views/Organizador/contabilidad/contadores.cshtml");
        }

        public IActionResult Cuenta()
        {
            return View("~/Views/Organizador/contabilidad/cuenta.cshtml");
        }

        public IActionResult Equipos()
        {
            return View();
        }

        public IActionResult GestionarEquipo()
        {
            return View("~/Views/Organizador/equipos/gestionarEquipo.cshtml");
        }

        public IActionResult Noticias()
        {
            return View();
        }
    }
}
