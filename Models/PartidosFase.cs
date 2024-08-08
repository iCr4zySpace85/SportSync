using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class PartidosFase
{
    public int IdPartidoFase { get; set; }

    public int IdPartido { get; set; }

    public int IdFase { get; set; }

    public virtual Fase IdFaseNavigation { get; set; } = null!;

    public virtual Partido IdPartidoNavigation { get; set; } = null!;
}
