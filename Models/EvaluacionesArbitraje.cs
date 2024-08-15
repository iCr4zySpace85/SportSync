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
    public int IdPartido { get; set; }
    public int IdTorneo { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Ubicacion { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public int IdEquipo { get; set; }

    public virtual Usuario IdArbitroNavigation { get; set; } = null!;

    public virtual AsignacionesArbitraje IdAsignacionNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoGanadorNavigation { get; set; } = null!;

    public virtual Equipo IdEquipoPerdedorNavigation { get; set; } = null!;
}
