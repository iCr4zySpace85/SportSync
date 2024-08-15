using SportSync.Models;
using System.Collections.Generic;

namespace SportSync.ViewModels
{
    public class TorneoViewModel
    {
        public Torneo Torneo { get; set; }
        public Dictionary<int, string> Deportes { get; set; }

        public List<Equipo> Equipos { get; set; }
    }
}
