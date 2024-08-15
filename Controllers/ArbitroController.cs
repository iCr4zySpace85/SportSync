using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportSync.Data;
using SportSync.Models;
using SportSync.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportSync.Controllers
{
    //[Authorize(Roles = "Árbitro")]
    public class ArbitroController : Controller
    {
        private readonly SportSyncContext _context;

        public ArbitroController(SportSyncContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario y su rol
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Verificar si el rol del usuario es "Árbitro" (rol 4)
            if (string.IsNullOrEmpty(userId) || userRole != "Árbitro")
            {
                // Maneja el caso donde el usuario no es un árbitro o el ID del usuario no está disponible
                return RedirectToAction("Error", "Home");
            }

            // Convertir el ID del usuario a entero
            if (!int.TryParse(userId, out int idArbitro))
            {
                return RedirectToAction("Error", "Home");
            }

            // Obtener los torneos asignados al árbitro
            var torneosAsignados = await _context.AsignacionesArbitrajes
                .Where(a => a.IdArbitro == idArbitro)
                .Include(a => a.IdPartidoNavigation)
                    .ThenInclude(p => p.IdTorneoNavigation)
                .Select(a => a.IdPartidoNavigation.IdTorneoNavigation)
                .Distinct()
                .ToListAsync();

            return View(torneosAsignados);
        }

        public async Task<IActionResult> PanelArbitro(int? idTorneo)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userId) || userRole != "Árbitro")
            {
                return RedirectToAction("Error", "Home");
            }

            if (!int.TryParse(userId, out int idArbitro))
            {
                return RedirectToAction("Error", "Home");
            }

            // Consulta base: Obtener las asignaciones del árbitro
            var asignacionesBase = _context.AsignacionesArbitrajes
                .Where(a => a.IdArbitro == idArbitro)
                .Include(a => a.IdPartidoNavigation)
                .ThenInclude(p => p.IdTorneoNavigation)
                .AsQueryable();

            // Filtrar por torneo si se proporciona idTorneo
            if (idTorneo.HasValue)
            {
                asignacionesBase = asignacionesBase
                    .Where(a => a.IdPartidoNavigation.IdTorneo == idTorneo.Value);
            }

            // Ejecutar la consulta y proyectar los resultados
            var asignaciones = await asignacionesBase
                .Select(a => new
                {
                    Fecha = a.IdPartidoNavigation.Fecha,
                    Hora = a.IdPartidoNavigation.Hora,
                    NombreTorneo = a.IdPartidoNavigation.IdTorneoNavigation.Nombre,
                    EquipoLocal = a.IdPartidoNavigation.IdEquipoLocalNavigation.Nombre,
                    EquipoVisitante = a.IdPartidoNavigation.IdEquipoVisitanteNavigation.Nombre,
                    Ubicacion = a.IdPartidoNavigation.Ubicacion
                })
                .ToListAsync();

            return View(asignaciones);
        }

      



    }
}
