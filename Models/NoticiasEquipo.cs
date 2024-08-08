using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class NoticiasEquipo
{
    public int IdNoticiaEquipo { get; set; }

    public int IdNoticia { get; set; }

    public int IdEquipo { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;

    public virtual Noticia IdNoticiaNavigation { get; set; } = null!;
}
