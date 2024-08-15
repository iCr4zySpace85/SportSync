//using CrafterCodes.Data;
//using CrafterCodes.Models;
//using CrafterCodes.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportSync.Data;
using SportSync.Models;
using SportSync.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SportSync.Data.Servicios;

namespace CrafterCodes.Controllers
{
    //[Authorize(Roles = "Organizador, Coach, Administrador")]
    public class CoachController : Controller
    {

        private readonly SportsyncContext _context;

        public CoachController(SportsyncContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario y su rol
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Verificar si el rol del usuario es "Coach" (rol 3)
            if (string.IsNullOrEmpty(userId) || userRole != "Coach")
            {
                // Maneja el caso donde el usuario no es un coach o el ID del usuario no está disponible
                return RedirectToAction("Error", "Home");
            }

            // Convertir el ID del usuario a entero
            if (!int.TryParse(userId, out int idCoach))
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener los equipos asignados al coach
            var equiposAsignados = await _context.Equipos
                .Where(e => e.IdCoach == idCoach)
                .Select(e => e.IdEquipo)
                .ToListAsync();

            // Obtener los torneos en los que participan esos equipos
            var torneosAsignados = await _context.EquiposTorneos
                .Where(et => equiposAsignados.Contains(et.IdEquipo))
                .Include(et => et.IdTorneoNavigation)
                .Select(et => et.IdTorneoNavigation)
                .Distinct()
                .ToListAsync();

            return View(torneosAsignados);
        }



        public async Task<IActionResult> PanelEquipoCoach(int idTorneo)
        {
            // Obtener el ID del usuario y su rol
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Verificar si el rol del usuario es "Coach" (rol 3)
            if (string.IsNullOrEmpty(userId) || userRole != "Coach")
            {
                return RedirectToAction("Error", "Home");
            }

            // Convertir el ID del usuario a entero
            if (!int.TryParse(userId, out int idCoach))
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener el equipo asignado al coach para el torneo específico
            var equipoAsignado = await _context.EquiposTorneos
                .Where(et => et.IdTorneo == idTorneo)
                .Include(et => et.IdEquipoNavigation)
                .Where(et => et.IdEquipoNavigation.IdCoach == idCoach)
                .Select(et => et.IdEquipoNavigation)
                .FirstOrDefaultAsync();

            if (equipoAsignado == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener los jugadores del equipo asignado
            var jugadores = await _context.Jugadores
                .Where(j => j.IdEquipo == equipoAsignado.IdEquipo)
                .ToListAsync();

            

            // Preparar el modelo de vista
            var viewModel = new PanelEquipoCoach
            {
                //TorneoNombre = equipoAsignado.IdTorneoNavigation.Nombre,
                Categoria = equipoAsignado.Categoria,
                NumeroJugadores = jugadores.Count,
                Jugadores = jugadores
            };

            return View(viewModel);
        }

        public async Task<IActionResult> PartidosEquipo(int idTorneo)
        {
            // Obtener el ID del usuario y su rol
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Verificar si el rol del usuario es "Coach" (rol 3)
            if (string.IsNullOrEmpty(userId) || userRole != "Coach")
            {
                return RedirectToAction("Error", "Home");
            }

            // Convertir el ID del usuario a entero
            if (!int.TryParse(userId, out int idCoach))
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener el equipo asignado al coach para el torneo específico
            var equipoAsignado = await _context.EquiposTorneos
                .Where(et => et.IdTorneo == idTorneo)
                .Include(et => et.IdEquipoNavigation)
                .Where(et => et.IdEquipoNavigation.IdCoach == idCoach)
                .Select(et => et.IdEquipoNavigation)
                .FirstOrDefaultAsync();

            if (equipoAsignado == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener los partidos del equipo en el torneo
            var partidos = await _context.Partidos
                .Where(p => p.IdTorneo == idTorneo &&
                            (p.IdEquipoLocal == equipoAsignado.IdEquipo ||
                             p.IdEquipoVisitante == equipoAsignado.IdEquipo))
                .Include(p => p.IdEquipoLocalNavigation)
                .Include(p => p.IdEquipoVisitanteNavigation)
                .ToListAsync();

            // Obtener la fecha actual
            var fechaActual = DateOnly.FromDateTime(DateTime.Now);

            // Separar los partidos en pendientes y finalizados
            var partidosPendientes = partidos.Where(p => p.Fecha >= fechaActual).ToList();
            var partidosFinalizados = partidos.Where(p => p.Fecha < fechaActual).ToList();

            // Pasar los datos a la vista usando ViewData
            ViewData["IdEquipo"] = equipoAsignado.IdEquipo;
            ViewData["EquipoNombre"] = equipoAsignado.Nombre;
            ViewData["PartidosPendientes"] = partidosPendientes;
            ViewData["PartidosFinalizados"] = partidosFinalizados;

            return View();
        }



    }
}
