using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class ResultadosTorneo
{
    public int IdResultado { get; set; }

    public int IdTorneo { get; set; }

    public int? IdEquipoGanador { get; set; }

    public int? IdEquipoSubcampeon { get; set; }

    public int? IdEquipoTercero { get; set; }

    public DateOnly? FechaResultado { get; set; }

    public int IdOrganizador { get; set; }

    public virtual Equipo? IdEquipoGanadorNavigation { get; set; }

    public virtual Equipo? IdEquipoSubcampeonNavigation { get; set; }

    public virtual Equipo? IdEquipoTerceroNavigation { get; set; }

    public virtual Usuario IdOrganizadorNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
