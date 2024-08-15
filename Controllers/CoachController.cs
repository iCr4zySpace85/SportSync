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

        private readonly Contexto _context;
        private readonly CoachServicio _coachServicio;
        public  CoachController(Contexto context)
        {
            _context = context;
            _coachServicio = new CoachServicio(context);
        }

        public async Task<IActionResult> Index()
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

            // Obtener los torneos utilizando el servicio
            var torneosAsignados = await _coachServicio.ObtenerTorneosPorCoach(idCoach);

            return View(torneosAsignados);
        }

        public IActionResult PanelEquipoCoach()
        {
            return View();
        }
        
        public IActionResult PartidosEquipo()
        {
            return View();
        }

    }
}
