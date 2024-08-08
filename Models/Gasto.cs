using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Gasto
{
    public int IdGasto { get; set; }

    public int IdTorneo { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public DateOnly? Fecha { get; set; }

    public int IdContador { get; set; }

    public virtual Usuario IdContadorNavigation { get; set; } = null!;

    public virtual Torneo IdTorneoNavigation { get; set; } = null!;
}
