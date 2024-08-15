using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportSync.Models;

public partial class SportsyncContext : DbContext
{
    public SportsyncContext()
    {
    }

    public SportsyncContext(DbContextOptions<SportsyncContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arbitraje> Arbitrajes { get; set; }

    public virtual DbSet<AsignacionesArbitraje> AsignacionesArbitrajes { get; set; }

    public virtual DbSet<AuditoriaAb> AuditoriaAbs { get; set; }

    public virtual DbSet<AuditoriaAc> AuditoriaAcs { get; set; }

    public virtual DbSet<Deporte> Deportes { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<EquiposTorneo> EquiposTorneos { get; set; }

    public virtual DbSet<EvaluacionesArbitraje> EvaluacionesArbitrajes { get; set; }

    public virtual DbSet<Fase> Fases { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Jugadore> Jugadores { get; set; }

    public virtual DbSet<Noticia> Noticias { get; set; }

    public virtual DbSet<NoticiasEquipo> NoticiasEquipos { get; set; }

    public virtual DbSet<NoticiasTorneo> NoticiasTorneos { get; set; }

    public virtual DbSet<Partido> Partidos { get; set; }

    public virtual DbSet<PartidosFase> PartidosFases { get; set; }

    public virtual DbSet<Patrocinadore> Patrocinadores { get; set; }

    public virtual DbSet<ResultadosTorneo> ResultadosTorneos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Torneo> Torneos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cqq0utaj1k6c73d93oe0-a.oregon-postgres.render.com;Database=sportsync;Username=sportsync_user;Password=minOhTcLcwlblAVSIPv4DTAg5U6PWeja");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arbitraje>(entity =>
        {
            entity.HasKey(e => e.IdArbitraje).HasName("arbitraje_pkey");

            entity.ToTable("arbitraje");

            entity.Property(e => e.IdArbitraje).HasColumnName("id_arbitraje");
            entity.Property(e => e.Cantidad)
                .HasPrecision(10, 2)
                .HasColumnName("cantidad");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.IdContador).HasColumnName("id_contador");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.IdPartido).HasColumnName("id_partido");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");

            entity.HasOne(d => d.IdContadorNavigation).WithMany(p => p.Arbitrajes)
                .HasForeignKey(d => d.IdContador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("arbitraje_id_contador_fkey");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Arbitrajes)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("arbitraje_id_equipo_fkey");

            entity.HasOne(d => d.IdPartidoNavigation).WithMany(p => p.Arbitrajes)
                .HasForeignKey(d => d.IdPartido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("arbitraje_id_partido_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Arbitrajes)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("arbitraje_id_torneo_fkey");
        });

        modelBuilder.Entity<AsignacionesArbitraje>(entity =>
        {
            entity.HasKey(e => e.IdAsignacion).HasName("asignaciones_arbitraje_pkey");

            entity.ToTable("asignaciones_arbitraje");

            entity.Property(e => e.IdAsignacion).HasColumnName("id_asignacion");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.IdArbitro).HasColumnName("id_arbitro");
            entity.Property(e => e.IdPartido).HasColumnName("id_partido");

            entity.HasOne(d => d.IdArbitroNavigation).WithMany(p => p.AsignacionesArbitrajes)
                .HasForeignKey(d => d.IdArbitro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_arbitraje_id_arbitro_fkey");

            entity.HasOne(d => d.IdPartidoNavigation).WithMany(p => p.AsignacionesArbitrajes)
                .HasForeignKey(d => d.IdPartido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_arbitraje_id_partido_fkey");
        });

        modelBuilder.Entity<AuditoriaAb>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("auditoria_ab_pkey");

            entity.ToTable("auditoria_ab");

            entity.Property(e => e.IdAuditoria).HasColumnName("id_auditoria");
            entity.Property(e => e.CampoAfectado)
                .HasMaxLength(50)
                .HasColumnName("campo_afectado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(50)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.TipoOperacion)
                .HasMaxLength(10)
                .HasColumnName("tipo_operacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.ValorAnterior).HasColumnName("valor_anterior");
            entity.Property(e => e.ValorNuevo).HasColumnName("valor_nuevo");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AuditoriaAbs)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditoria_ab_usuario_id_fkey");
        });

        modelBuilder.Entity<AuditoriaAc>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("auditoria_ac_pkey");

            entity.ToTable("auditoria_ac");

            entity.Property(e => e.IdAuditoria).HasColumnName("id_auditoria");
            entity.Property(e => e.CampoAfectado)
                .HasMaxLength(50)
                .HasColumnName("campo_afectado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(50)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.TipoOperacion)
                .HasMaxLength(10)
                .HasColumnName("tipo_operacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.ValorAnterior).HasColumnName("valor_anterior");
            entity.Property(e => e.ValorNuevo).HasColumnName("valor_nuevo");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AuditoriaAcs)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditoria_ac_usuario_id_fkey");
        });

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.IdDeporte).HasName("deportes_pkey");

            entity.ToTable("deportes");

            entity.Property(e => e.IdDeporte).HasColumnName("id_deporte");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("equipos_pkey");

            entity.ToTable("equipos");

            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .HasColumnName("categoria");
            entity.Property(e => e.IdCoach).HasColumnName("id_coach");
            entity.Property(e => e.IdDeporte).HasColumnName("id_deporte");
            entity.Property(e => e.ImgEquipo).HasColumnName("img_equipo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCoachNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdCoach)
                .HasConstraintName("fk_equipos_usuarios");

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipos_id_deporte_fkey");
        });

        modelBuilder.Entity<EquiposTorneo>(entity =>
        {
            entity.HasKey(e => e.IdEquipoTorneo).HasName("equipos_torneos_pkey");

            entity.ToTable("equipos_torneos");

            entity.Property(e => e.IdEquipoTorneo).HasColumnName("id_equipo_torneo");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.EquiposTorneos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipos_torneos_id_equipo_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.EquiposTorneos)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipos_torneos_id_torneo_fkey");
        });

        modelBuilder.Entity<EvaluacionesArbitraje>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("evaluaciones_arbitraje_pkey");

            entity.ToTable("evaluaciones_arbitraje");

            entity.Property(e => e.IdEvaluacion).HasColumnName("id_evaluacion");
            entity.Property(e => e.IdArbitro).HasColumnName("id_arbitro");
            entity.Property(e => e.IdAsignacion).HasColumnName("id_asignacion");
            entity.Property(e => e.IdEquipoGanador).HasColumnName("id_equipo_ganador");
            entity.Property(e => e.IdEquipoPerdedor).HasColumnName("id_equipo_perdedor");
            entity.Property(e => e.ResultadoEquipoGanador).HasColumnName("resultado_equipo_ganador");
            entity.Property(e => e.ResultadoEquipoPerdedor).HasColumnName("resultado_equipo_perdedor");

            entity.HasOne(d => d.IdArbitroNavigation).WithMany(p => p.EvaluacionesArbitrajes)
                .HasForeignKey(d => d.IdArbitro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("evaluaciones_arbitraje_id_arbitro_fkey");

            entity.HasOne(d => d.IdAsignacionNavigation).WithMany(p => p.EvaluacionesArbitrajes)
                .HasForeignKey(d => d.IdAsignacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("evaluaciones_arbitraje_id_asignacion_fkey");

            entity.HasOne(d => d.IdEquipoGanadorNavigation).WithMany(p => p.EvaluacionesArbitrajeIdEquipoGanadorNavigations)
                .HasForeignKey(d => d.IdEquipoGanador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("evaluaciones_arbitraje_id_equipo_ganador_fkey");

            entity.HasOne(d => d.IdEquipoPerdedorNavigation).WithMany(p => p.EvaluacionesArbitrajeIdEquipoPerdedorNavigations)
                .HasForeignKey(d => d.IdEquipoPerdedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("evaluaciones_arbitraje_id_equipo_perdedor_fkey");
        });

        modelBuilder.Entity<Fase>(entity =>
        {
            entity.HasKey(e => e.IdFase).HasName("fases_pkey");

            entity.ToTable("fases");

            entity.Property(e => e.IdFase).HasColumnName("id_fase");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");
            entity.Property(e => e.NombreFase)
                .HasMaxLength(255)
                .HasColumnName("nombre_fase");
            entity.Property(e => e.TipoFase)
                .HasMaxLength(50)
                .HasColumnName("tipo_fase");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Fases)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fases_id_torneo_fkey");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.IdGasto).HasName("gastos_pkey");

            entity.ToTable("gastos");

            entity.Property(e => e.IdGasto).HasColumnName("id_gasto");
            entity.Property(e => e.Concepto)
                .HasMaxLength(255)
                .HasColumnName("concepto");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdContador).HasColumnName("id_contador");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdContadorNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdContador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_id_contador_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gastos_id_torneo_fkey");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("inscripciones_pkey");

            entity.ToTable("inscripciones");

            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdContador).HasColumnName("id_contador");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");

            entity.HasOne(d => d.IdContadorNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdContador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inscripciones_id_contador_fkey");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inscripciones_id_equipo_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inscripciones_id_torneo_fkey");
        });

        modelBuilder.Entity<Jugadore>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("jugadores_pkey");

            entity.ToTable("jugadores");

            entity.Property(e => e.IdJugador).HasColumnName("id_jugador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.ImgJugador).HasColumnName("img_jugador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Posicion)
                .HasMaxLength(50)
                .HasColumnName("posicion");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jugadores_id_equipo_fkey");
        });

        modelBuilder.Entity<Noticia>(entity =>
        {
            entity.HasKey(e => e.IdNoticia).HasName("noticias_pkey");

            entity.ToTable("noticias");

            entity.Property(e => e.IdNoticia).HasColumnName("id_noticia");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_publicacion");
            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Noticia)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noticias_id_autor_fkey");
        });

        modelBuilder.Entity<NoticiasEquipo>(entity =>
        {
            entity.HasKey(e => e.IdNoticiaEquipo).HasName("noticias_equipos_pkey");

            entity.ToTable("noticias_equipos");

            entity.Property(e => e.IdNoticiaEquipo).HasColumnName("id_noticia_equipo");
            entity.Property(e => e.IdEquipo).HasColumnName("id_equipo");
            entity.Property(e => e.IdNoticia).HasColumnName("id_noticia");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.NoticiasEquipos)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noticias_equipos_id_equipo_fkey");

            entity.HasOne(d => d.IdNoticiaNavigation).WithMany(p => p.NoticiasEquipos)
                .HasForeignKey(d => d.IdNoticia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noticias_equipos_id_noticia_fkey");
        });

        modelBuilder.Entity<NoticiasTorneo>(entity =>
        {
            entity.HasKey(e => e.IdNoticiaTorneo).HasName("noticias_torneos_pkey");

            entity.ToTable("noticias_torneos");

            entity.Property(e => e.IdNoticiaTorneo).HasColumnName("id_noticia_torneo");
            entity.Property(e => e.IdNoticia).HasColumnName("id_noticia");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");

            entity.HasOne(d => d.IdNoticiaNavigation).WithMany(p => p.NoticiasTorneos)
                .HasForeignKey(d => d.IdNoticia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noticias_torneos_id_noticia_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.NoticiasTorneos)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("noticias_torneos_id_torneo_fkey");
        });

        modelBuilder.Entity<Partido>(entity =>
        {
            entity.HasKey(e => e.IdPartido).HasName("partidos_pkey");

            entity.ToTable("partidos");

            entity.Property(e => e.IdPartido).HasColumnName("id_partido");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdEquipoLocal).HasColumnName("id_equipo_local");
            entity.Property(e => e.IdEquipoVisitante).HasColumnName("id_equipo_visitante");
            entity.Property(e => e.IdOrganizador).HasColumnName("id_organizador");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.IdEquipoLocalNavigation).WithMany(p => p.PartidoIdEquipoLocalNavigations)
                .HasForeignKey(d => d.IdEquipoLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_id_equipo_local_fkey");

            entity.HasOne(d => d.IdEquipoVisitanteNavigation).WithMany(p => p.PartidoIdEquipoVisitanteNavigations)
                .HasForeignKey(d => d.IdEquipoVisitante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_id_equipo_visitante_fkey");

            entity.HasOne(d => d.IdOrganizadorNavigation).WithMany(p => p.Partidos)
                .HasForeignKey(d => d.IdOrganizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_id_organizador_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Partidos)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_id_torneo_fkey");
        });

        modelBuilder.Entity<PartidosFase>(entity =>
        {
            entity.HasKey(e => e.IdPartidoFase).HasName("partidos_fase_pkey");

            entity.ToTable("partidos_fase");

            entity.Property(e => e.IdPartidoFase).HasColumnName("id_partido_fase");
            entity.Property(e => e.IdFase).HasColumnName("id_fase");
            entity.Property(e => e.IdPartido).HasColumnName("id_partido");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.PartidosFases)
                .HasForeignKey(d => d.IdFase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_fase_id_fase_fkey");

            entity.HasOne(d => d.IdPartidoNavigation).WithMany(p => p.PartidosFases)
                .HasForeignKey(d => d.IdPartido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partidos_fase_id_partido_fkey");
        });

        modelBuilder.Entity<Patrocinadore>(entity =>
        {
            entity.HasKey(e => e.IdPatrocinador).HasName("patrocinadores_pkey");

            entity.ToTable("patrocinadores");

            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.Concepto)
                .HasMaxLength(255)
                .HasColumnName("concepto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.IdContador).HasColumnName("id_contador");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoApoyo)
                .HasMaxLength(255)
                .HasColumnName("tipo_apoyo");

            entity.HasOne(d => d.IdContadorNavigation).WithMany(p => p.Patrocinadores)
                .HasForeignKey(d => d.IdContador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patrocinadores_id_contador_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.Patrocinadores)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patrocinadores_id_torneo_fkey");
        });

        modelBuilder.Entity<ResultadosTorneo>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("resultados_torneo_pkey");

            entity.ToTable("resultados_torneo");

            entity.Property(e => e.IdResultado).HasColumnName("id_resultado");
            entity.Property(e => e.FechaResultado).HasColumnName("fecha_resultado");
            entity.Property(e => e.IdEquipoGanador).HasColumnName("id_equipo_ganador");
            entity.Property(e => e.IdEquipoSubcampeon).HasColumnName("id_equipo_subcampeon");
            entity.Property(e => e.IdEquipoTercero).HasColumnName("id_equipo_tercero");
            entity.Property(e => e.IdOrganizador).HasColumnName("id_organizador");
            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");

            entity.HasOne(d => d.IdEquipoGanadorNavigation).WithMany(p => p.ResultadosTorneoIdEquipoGanadorNavigations)
                .HasForeignKey(d => d.IdEquipoGanador)
                .HasConstraintName("resultados_torneo_id_equipo_ganador_fkey");

            entity.HasOne(d => d.IdEquipoSubcampeonNavigation).WithMany(p => p.ResultadosTorneoIdEquipoSubcampeonNavigations)
                .HasForeignKey(d => d.IdEquipoSubcampeon)
                .HasConstraintName("resultados_torneo_id_equipo_subcampeon_fkey");

            entity.HasOne(d => d.IdEquipoTerceroNavigation).WithMany(p => p.ResultadosTorneoIdEquipoTerceroNavigations)
                .HasForeignKey(d => d.IdEquipoTercero)
                .HasConstraintName("resultados_torneo_id_equipo_tercero_fkey");

            entity.HasOne(d => d.IdOrganizadorNavigation).WithMany(p => p.ResultadosTorneos)
                .HasForeignKey(d => d.IdOrganizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_torneo_id_organizador_fkey");

            entity.HasOne(d => d.IdTorneoNavigation).WithMany(p => p.ResultadosTorneos)
                .HasForeignKey(d => d.IdTorneo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("resultados_torneo_id_torneo_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Torneo>(entity =>
        {
            entity.HasKey(e => e.IdTorneo).HasName("torneos_pkey");

            entity.ToTable("torneos");

            entity.Property(e => e.IdTorneo).HasColumnName("id_torneo");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .HasColumnName("categoria");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdDeporte).HasColumnName("id_deporte");
            entity.Property(e => e.ImgTorneo)
                .HasColumnType("character varying")
                .HasColumnName("img_torneo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdDeporteNavigation).WithMany(p => p.Torneos)
                .HasForeignKey(d => d.IdDeporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("torneos_id_deporte_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "usuarios_correo_key").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ApMaterno)
                .HasMaxLength(50)
                .HasColumnName("ap_materno");
            entity.Property(e => e.ApPaterno)
                .HasMaxLength(50)
                .HasColumnName("ap_paterno");
            entity.Property(e => e.CodigoVerificación)
                .HasMaxLength(15)
                .HasColumnName("codigo_verificación");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreCelular)
                .HasMaxLength(15)
                .HasColumnName("nombre_celular");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(13)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Pass).HasColumnName("pass");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_id_rol_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
