using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Fase
{
    public int IdFase { get; set; }

    public int IdTorneo { get; set; }

    public string NombreFase { get; set; } = null!;

    public string TipoFase { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;

    public virtual ICollection<PartidosFase> PartidosFases { get; set; } = new List<PartidosFase>();
}
