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

namespace CrafterCodes.Controllers
{
    //[Authorize(Roles = "Organizador, Coach, Administrador")]
    public class CoachController : Controller
    {

        private readonly SportSyncContext _context;

        public CoachController(SportSyncContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
