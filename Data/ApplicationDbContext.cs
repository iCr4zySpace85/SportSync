using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SportSync.Models;


namespace SportSync.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }



        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugadore> Jugadores { get; set; }
        public DbSet<EquiposTorneo> EquiposTorneos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Fase> Fases { get; set; }
        public DbSet<PartidosFase> PartidosFases { get; set; }
        public DbSet<AsignacionesArbitraje> AsignacionesArbitraje { get; set; }
        public DbSet<EvaluacionesArbitraje> EvaluacionesArbitraje { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<NoticiasEquipo> NoticiasEquipos { get; set; }
        public DbSet<NoticiasTorneo> NoticiasTorneos { get; set; }
        public DbSet<Patrocinadore> Patrocinadores { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Inscripcione> Inscripciones { get; set; }
        public DbSet<Arbitraje> Arbitrajes { get; set; }
        public DbSet<ResultadosTorneo> ResultadosTorneo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de las relaciones y otras configuraciones aquí.
            base.OnModelCreating(modelBuilder);
        }
    }
}
