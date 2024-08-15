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
using Microsoft.AspNetCore.Mvc.Rendering;
using SportSync.Models.ViewModels;

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
            var deportes = _context.Deportes.Select(d => new SelectListItem
            {
                Value = d.IdDeporte.ToString(),
                Text = d.Nombre,
                Selected = d.IdDeporte == torneo.IdDeporte // Marca el deporte seleccionado actualmente
            }).ToList();

            ViewBag.Deportes = deportes;

            var equipos = _context.Equipos.Select(e => new SelectListItem
            {
                Value = e.IdEquipo.ToString(),
                Text = e.Nombre
            }).ToList();

            ViewBag.Equipos = equipos;

            // Obtiene la lista de equipos asociados al torneo
            var equiposAsociados = torneo.EquiposTorneos
                .Select(et => et.IdEquipoNavigation)
                .ToList();

            var model = new TorneoViewModel
            {
                Torneo = torneo,
                Equipos = equiposAsociados
            };

            return View(model);
        }


        // Acción para guardar los cambios del torneo
        [HttpPost]
        public IActionResult GuardarCambios(Torneo torneo)
        {
            if (torneo.Nombre != null)
            {
                _context.Update(torneo);
                _context.SaveChanges();
                return RedirectToAction("GestionarTorneo", new { id = torneo.IdTorneo });
            }
            else { 
                return View("GestionarTorneo", new { id = torneo.IdTorneo });
            }

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarTorneo(int idTorneo)
        {
            var torneo = await _context.Torneos.FindAsync(idTorneo);
            if (torneo == null)
            {
                return NotFound();
            }

            _context.Torneos.Remove(torneo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // O a otra acción que desees
        }


        // Acción para agregar un nuevo equipo
        // Acción para agregar un nuevo equipo
        [HttpPost]
        public async Task<IActionResult> AgregarEquipo(int idTorneo, int idEquipo)
        {
            

            

            var equipoTorneo = new EquiposTorneo
            {
                IdTorneo = idTorneo,
                IdEquipo = idEquipo
            };

            _context.EquiposTorneos.Add(equipoTorneo);
            await _context.SaveChangesAsync();

            return RedirectToAction("GestionarTorneo", new { id = idTorneo });
        }




        [HttpPost]
        public IActionResult EliminarEquipo(int equipoId, int torneoId)
        {
            // Busca la relación equipo-torneo por los IDs
            var equipoTorneo = _context.EquiposTorneos
                .FirstOrDefault(et => et.IdEquipo == equipoId && et.IdTorneo == torneoId);

            if (equipoTorneo == null)
            {
                return NotFound();
            }

            // Elimina la relación entre el equipo y el torneo
            _context.EquiposTorneos.Remove(equipoTorneo);
            _context.SaveChanges();

            // Redirige a la acción de gestionar torneo
            return RedirectToAction("GestionarTorneo", new { id = torneoId });
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

        public async Task<IActionResult> Coaches()
        {
            // Obtén el id del rol de Coach
            var coachRoleId = await _context.Roles
                .Where(r => r.NombreRol == "Coach")
                .Select(r => r.IdRol)
                .FirstOrDefaultAsync();

            if (coachRoleId == 0)
            {
                return NotFound("Rol de Coach no encontrado.");
            }

            // Consulta los usuarios que tienen el rol de Coach, incluyendo su equipo
            var coaches = await _context.Usuarios
                .Include(u => u.Equipos)
                .Where(u => u.IdRol == coachRoleId)
                .ToListAsync();

            var model = new CoachViewModel
            {
                Usuarios = coaches
            };

            return View(model);
        }

        public IActionResult Arbitros()
        {
            var arbitros = _context.Usuarios.Where(u => u.IdRol == 4).ToList();
            var model = new UsuarioViewModel
            {
                Usuarios = arbitros
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CrearArbitro(UsuarioViewModel model)
        {
            if (model != null)
            {
                var nuevoArbitro = model.NuevoUsuario;
                nuevoArbitro.IdRol = 4; // Asignar el rol de árbitro
                nuevoArbitro.FechaCreacion = DateTime.Now;

                _context.Usuarios.Add(nuevoArbitro);
                await _context.SaveChangesAsync();

                return RedirectToAction("Arbitros"); // Redirigir a la lista de árbitros después de la creación
            }

            // Si la validación falla, devolver la vista con el modelo actual y la lista de árbitros
            var arbitros = _context.Usuarios.Where(u => u.IdRol == 4).ToList();
            var viewModel = new UsuarioViewModel
            {
                Usuarios = arbitros
            };

            return View("Arbitros", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarArbitro(int idUsuario)
        {
            // Busca el árbitro por su ID
            var arbitro = await _context.Usuarios.FindAsync(idUsuario);
            if (arbitro == null || arbitro.IdRol != 4) // Asegúrate de que el usuario es un árbitro
            {
                return NotFound();
            }

            // Elimina el árbitro de la base de datos
            _context.Usuarios.Remove(arbitro);
            await _context.SaveChangesAsync();

            // Redirige a la lista de árbitros
            return RedirectToAction("Arbitros");
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

        public async Task<IActionResult> Equipos()
        {
            var equipos = await _context.Equipos
                .Include(e => e.IdCoachNavigation) // Incluir la relación con el Coach
                .Include(e => e.IdDeporteNavigation) // Incluir la relación con Deporte si lo necesitas
                .ToListAsync();

            return View(equipos);
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
