using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class EquiposTorneo
{
    public int IdEquipoTorneo { get; set; }

    public int IdEquipo { get; set; }

    public int IdTorneo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
