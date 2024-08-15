using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Jugadore
{
    public int IdJugador { get; set; }

    public byte[]? ImgJugador { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEquipo { get; set; }

    public string? Posicion { get; set; }

    public int Edad { get; set; }

    public int? Numero { get; set; }

    public string? Descripcion { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; } = null!;
}
