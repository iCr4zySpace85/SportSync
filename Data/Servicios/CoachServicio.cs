using Npgsql;
using SportSync.Models;

namespace SportSync.Data.Servicios
{
    public class CoachServicio
    {
        private readonly Contexto _context;

        public CoachServicio(Contexto context)
        {
            _context = context;
        }

        public async Task<List<Torneo>> ObtenerTorneosPorCoach(int idCoach)
        {
            var torneos = new List<Torneo>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    var sql = @"
                    SELECT DISTINCT t.*
                    FROM Equipos e
                    JOIN Equipos_Torneos et ON e.id_equipo = et.id_equipo
                    JOIN Torneos t ON et.id_torneo = t.id_torneo
                    WHERE e.id_coach = @IdCoach";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("IdCoach", idCoach);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var torneo = new Torneo
                                {
                                    IdTorneo = reader.GetInt32(reader.GetOrdinal("ID_torneo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("Fecha_inicio")),
                                    FechaFin = reader.GetDateTime(reader.GetOrdinal("Fecha_fin")),
                                    IdDeporte = reader.GetInt32(reader.GetOrdinal("ID_deporte"))
                                };
                                torneos.Add(torneo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registra el error, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return torneos;
        }
    }
}
