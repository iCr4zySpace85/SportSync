using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class NoticiasTorneo
{
    public int IdNoticiaTorneo { get; set; }

    public int IdNoticia { get; set; }

    public int IdTorneo { get; set; }

    public virtual Noticia IdNoticiaNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
