using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApPaterno { get; set; }

    public string? ApMaterno { get; set; }

    public string Correo { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public string? NombreCelular { get; set; }

    public string? CodigoVerificación { get; set; }

    public int IdRol { get; set; }

    public string? NombreUsuario { get; set; }

    public virtual ICollection<Arbitraje> Arbitrajes { get; set; } = new List<Arbitraje>();

    public virtual ICollection<AsignacionesArbitraje> AsignacionesArbitrajes { get; set; } = new List<AsignacionesArbitraje>();

    public virtual ICollection<AuditoriaAb> AuditoriaAbs { get; set; } = new List<AuditoriaAb>();

    public virtual ICollection<AuditoriaAc> AuditoriaAcs { get; set; } = new List<AuditoriaAc>();

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<EvaluacionesArbitraje> EvaluacionesArbitrajes { get; set; } = new List<EvaluacionesArbitraje>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Noticia> Noticia { get; set; } = new List<Noticia>();

    public virtual ICollection<Partido> Partidos { get; set; } = new List<Partido>();

    public virtual ICollection<Patrocinadore> Patrocinadores { get; set; } = new List<Patrocinadore>();

    public virtual ICollection<ResultadosTorneo> ResultadosTorneos { get; set; } = new List<ResultadosTorneo>();
}
