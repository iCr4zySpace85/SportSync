using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class Equipo
{
    public int IdEquipo { get; set; }

    public byte[]? ImgEquipo { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdDeporte { get; set; }

    public string Categoria { get; set; } = null!;

    public int? IdCoach { get; set; }


    public virtual ICollection<Arbitraje> Arbitrajes { get; set; } = new List<Arbitraje>();

    public virtual ICollection<EquiposTorneo> EquiposTorneos { get; set; } = new List<EquiposTorneo>();

    public virtual ICollection<EvaluacionesArbitraje> EvaluacionesArbitrajeIdEquipoGanadorNavigations { get; set; } = new List<EvaluacionesArbitraje>();

    public virtual ICollection<EvaluacionesArbitraje> EvaluacionesArbitrajeIdEquipoPerdedorNavigations { get; set; } = new List<EvaluacionesArbitraje>();

    public virtual Usuario? IdCoachNavigation { get; set; }

    public virtual Deporte IdDeporteNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Jugadore> Jugadores { get; set; } = new List<Jugadore>();

    public virtual ICollection<NoticiasEquipo> NoticiasEquipos { get; set; } = new List<NoticiasEquipo>();

    public virtual ICollection<Partido> PartidoIdEquipoLocalNavigations { get; set; } = new List<Partido>();

    public virtual ICollection<Partido> PartidoIdEquipoVisitanteNavigations { get; set; } = new List<Partido>();

    public virtual ICollection<ResultadosTorneo> ResultadosTorneoIdEquipoGanadorNavigations { get; set; } = new List<ResultadosTorneo>();

    public virtual ICollection<ResultadosTorneo> ResultadosTorneoIdEquipoSubcampeonNavigations { get; set; } = new List<ResultadosTorneo>();

    public virtual ICollection<ResultadosTorneo> ResultadosTorneoIdEquipoTerceroNavigations { get; set; } = new List<ResultadosTorneo>();
}
