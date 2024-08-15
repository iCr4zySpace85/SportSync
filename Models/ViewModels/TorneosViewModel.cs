using SportSync.Models;
using System.Collections.Generic;

namespace SportSync.ViewModels
{
    public class TorneosViewModel
    {
        public IEnumerable<Torneo> Torneos { get; set; }
        public Dictionary<int, string> Deportes { get; set; }
    }
}
