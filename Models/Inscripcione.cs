using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Inscripcione
{
    public int IdInscripcion { get; set; }

    public int IdTorneo { get; set; }

    public int IdEquipo { get; set; }

    public string Estado { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int IdContador { get; set; }

    public virtual Usuario IdContadorNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
