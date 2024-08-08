using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Deporte
{
    public int IdDeporte { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<Torneo> Torneos { get; set; } = new List<Torneo>();
}
