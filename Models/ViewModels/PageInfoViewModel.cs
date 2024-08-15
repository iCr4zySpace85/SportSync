
using SportSync.Models;
namespace SportSync.Models.ViewModels
{
    public class PageInfoViewModel
    {
        public Deporte? Deporte { get; set; }
        public List<Torneo>? Torneos { get; set; }
        public string NombreTorneo { get; set; }
        public List<Torneo>? TorneosDeporte { get; set; }
        public List<Jugadore>? Jugadores { get; set; }
        public List<Equipo>? Equipos { get; set; }
        public List<EquiposTorneo>? EquipoTorneo { get; set; }
        public List<Noticia>? Noticias { get; set; }
        public List<Partido>? Partidos { get; set; }
        public List<EvaluacionesArbitraje>? EvaluacionesArbitraje { get; set; }
        public List<ResultadosTorneo>? ResultadosTorneos { get; set; }
    }
}