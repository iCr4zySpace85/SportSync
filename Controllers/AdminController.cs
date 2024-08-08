using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportSync.Data;
using SportSync.Models;
using SportSync.ViewModels;
using System.Threading.Tasks;

namespace SportSync.Controllers
{
    public class AdminController : Controller
    {
       private readonly SportSyncContext _context;

        public AdminController(SportSyncContext context)
        {
            _context = context;
        }

        // GET: Usuarios/Create
        public IActionResult Index()
        {
             var usuarios = _context.Usuarios.ToList();
            var roles = new Dictionary<int, string>
            {
                { 1, "Administrador" },
                { 2, "Organizador" },
                { 3, "Coach" },
                { 4, "Árbitro" },
                { 5, "Contador" }
            };

            var model = new UsuariosViewModel
            {
                Usuarios = usuarios,
                Roles = roles
            };
            return View(model);
        }
        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Usuario model)
        {
            if (model != null)
            {
                var usuario = new Usuario
                {
                    Nombre = model.Nombre,
                    ApPaterno = model.ApPaterno,
                    ApMaterno = model.ApMaterno,
                    Correo = model.Correo,
                    Pass = model.Pass,
                    FechaCreacion = DateTime.Now,
                    NombreCelular = model.NombreCelular,
                    CodigoVerificación = "12345", // Generar un valor real en producción
                    IdRol = model.IdRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestCreateUser()
        {
            var usuario = new Usuario
            {
                Nombre = "Test",
                ApPaterno = "Test",
                ApMaterno = "Test",
                Correo = "test@example.com",
                Pass = "password",
                FechaCreacion = DateTime.Now,
                NombreCelular = "1234567890",
                CodigoVerificación = "12345",
                IdRol = 1
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
