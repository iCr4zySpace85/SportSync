using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class EvaluacionesArbitraje
{
    public int IdEvaluacion { get; set; }

    public int IdAsignacion { get; set; }

    public int IdEquipoGanador { get; set; }

    public int IdEquipoPerdedor { get; set; }

    public int? ResultadoEquipoGanador { get; set; }

    public int? ResultadoEquipoPerdedor { get; set; }

    public int IdArbitro { get; set; }

    public virtual Usuario IdArbitroNavigation { get; set; } = null!;

    public virtual AsignacionesArbitraje IdAsignacionNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoGanadorNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoPerdedorNavigation { get; set; } = null!;
}
