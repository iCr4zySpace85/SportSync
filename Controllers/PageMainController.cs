using SportSync.Data;
using SportSync.Models;
using SportSync.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportSync.Data.Servicios;

namespace SportSync.Controllers
{
    public class PageMainController : Controller
    {
        private readonly Contexto _context;
        private readonly PageMainTorneosServicio _pageMainTorneosServicio;
        public PageMainController(Contexto context)
        {
            _context = context;
            _pageMainTorneosServicio = new PageMainTorneosServicio(context);
        }

        public async Task<IActionResult> Inicio(int? idDeporte)
        {
            // Obtén todos los torneos si no se proporciona un ID de deporte
            var torneos = idDeporte.HasValue
                ? await _pageMainTorneosServicio.ObtenerTorneoPorDeporte(idDeporte.Value)
                : await _pageMainTorneosServicio.ObtenerTorneos();

            var noticias =  await _pageMainTorneosServicio.ObtenerAllNoticias();

            var model = new PageInfoViewModel
            {
                Torneos = torneos,
                Noticias = noticias.Take(5).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Torneo(int idTorneo)
        {
            var equipos = await _pageMainTorneosServicio.ObtenerEquiposPorTorneo(idTorneo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorTorneo(idTorneo);

            var model = new PageInfoViewModel
            {
                Equipos = equipos,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdTorneo = idTorneo;

            return View(model);
        }

        public async Task<IActionResult> Partidos(int idTorneo)
        {
            var partidos = await _pageMainTorneosServicio.ObtenerPartidosPorTorneo(idTorneo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorTorneo(idTorneo);

            var model = new PageInfoViewModel
            {
                Partidos = partidos,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdTorneo = idTorneo;

            return View(model);
        }

        public async Task<IActionResult> Resultados(int idTorneo)
        {
            var resultados = await _pageMainTorneosServicio.ObtenerResultadosPorTorneo(idTorneo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorTorneo(idTorneo);

            var model = new PageInfoViewModel
            {
                EvaluacionesArbitraje = resultados,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdTorneo = idTorneo;

            return View(model);
        }

        public async Task<IActionResult> Equipo(int idEquipo)
        {
            var partidos = await _pageMainTorneosServicio.ObtenerPartidosPorEquipo(idEquipo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorEquipo(idEquipo);

            var model = new PageInfoViewModel
            {
                Partidos = partidos,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdEquipo = idEquipo;

            return View(model);
        }

        public async Task<IActionResult> ResultadosEquipo(int idEquipo)
        {
            var resultados = await _pageMainTorneosServicio.ObtenerResultadosPorEquipo(idEquipo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorEquipo(idEquipo);

            var model = new PageInfoViewModel
            {
                EvaluacionesArbitraje = resultados,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdEquipo = idEquipo;

            return View(model);
        }

        public async Task<IActionResult> Jugadores(int idEquipo)
        {
            var jugadores = await _pageMainTorneosServicio.ObtenerJugadores(idEquipo);
            var noticias = await _pageMainTorneosServicio.ObtenerNoticiasPorEquipo(idEquipo);

            var model = new PageInfoViewModel
            {
                Jugadores = jugadores,
                Noticias = noticias.Take(5).ToList()
            };

            ViewBag.IdEquipo = idEquipo;

            return View(model);
        }

    }
}
