using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class AuditoriaAb
{
    public int IdAuditoria { get; set; }

    public DateTime? Fecha { get; set; }

    public int UsuarioId { get; set; }

    public string TablaAfectada { get; set; } = null!;

    public string CampoAfectado { get; set; } = null!;

    public string? ValorAnterior { get; set; }

    public string? ValorNuevo { get; set; }

    public string TipoOperacion { get; set; } = null!;

    public string? Mensaje { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
