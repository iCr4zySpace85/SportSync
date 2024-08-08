using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Arbitraje
{
    public int IdArbitraje { get; set; }

    public int IdTorneo { get; set; }

    public int IdPartido { get; set; }

    public int IdEquipo { get; set; }

    public decimal Cantidad { get; set; }

    public string Estado { get; set; } = null!;

    public int IdContador { get; set; }

    public virtual Usuario IdContadorNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Partido IdPartidoNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
