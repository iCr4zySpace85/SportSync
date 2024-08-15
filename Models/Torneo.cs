using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Torneo
{
    public int IdTorneo { get; set; }

    public string? ImgTorneo { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdDeporte { get; set; }

    public string? Categoria { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual ICollection<Arbitraje> Arbitrajes { get; set; } = new List<Arbitraje>();

    public virtual ICollection<EquiposTorneo> EquiposTorneos { get; set; } = new List<EquiposTorneo>();

    public virtual ICollection<Fase> Fases { get; set; } = new List<Fase>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual Deporte IdDeporteNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<NoticiasTorneo> NoticiasTorneos { get; set; } = new List<NoticiasTorneo>();

    public virtual ICollection<Partido> Partidos { get; set; } = new List<Partido>();

    public virtual ICollection<Patrocinadore> Patrocinadores { get; set; } = new List<Patrocinadore>();

    public virtual ICollection<ResultadosTorneo> ResultadosTorneos { get; set; } = new List<ResultadosTorneo>();
}
