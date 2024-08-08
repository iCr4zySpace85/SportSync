using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class AsignacionesArbitraje
{
    public int IdAsignacion { get; set; }

    public int IdPartido { get; set; }

    public int IdArbitro { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual ICollection<EvaluacionesArbitraje> EvaluacionesArbitrajes { get; set; } = new List<EvaluacionesArbitraje>();

    public virtual Usuario IdArbitroNavigation { get; set; } = null!;

    public virtual Partido IdPartidoNavigation { get; set; } = null!;
}
