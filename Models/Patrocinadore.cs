using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Patrocinadore
{
    public int IdPatrocinador { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public string TipoApoyo { get; set; } = null!;

    public string? Telefono { get; set; }

    public int IdTorneo { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public int IdContador { get; set; }

    public virtual Usuario IdContadorNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
