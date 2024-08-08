using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Noticia
{
    public int IdNoticia { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Contenido { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public int IdAutor { get; set; }

    public virtual Usuario IdAutorNavigation { get; set; } = null!;

    public virtual ICollection<NoticiasEquipo> NoticiasEquipos { get; set; } = new List<NoticiasEquipo>();

    public virtual ICollection<NoticiasTorneo> NoticiasTorneos { get; set; } = new List<NoticiasTorneo>();
}
