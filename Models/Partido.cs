using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Partido
{
    public int IdPartido { get; set; }

    public int IdTorneo { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string Ubicacion { get; set; } = null!;
    public string NombreEquipoLocal { get; set; } = null!;

    public int IdEquipoLocal { get; set; }
    public string NombreEquipoVisitante { get; set; } = null!;

    public int IdEquipoVisitante { get; set; }

    public int IdOrganizador { get; set; }

    public string Estado { get; set; } = null!;
    public int IdEquipo { get; set; }

    public virtual ICollection<Arbitraje> Arbitrajes { get; set; } = new List<Arbitraje>();

    public virtual ICollection<AsignacionesArbitraje> AsignacionesArbitrajes { get; set; } = new List<AsignacionesArbitraje>();

    public virtual Equipo IdEquipoLocalNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoVisitanteNavigation { get; set; } = null!;

    public virtual Usuario IdOrganizadorNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;

    public virtual ICollection<PartidosFase> PartidosFases { get; set; } = new List<PartidosFase>();
}
