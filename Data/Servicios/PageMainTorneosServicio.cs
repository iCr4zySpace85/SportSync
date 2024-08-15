using Npgsql;
using SportSync.Models;
using SportSync.Models.ViewModels;

namespace SportSync.Data.Servicios
{
    public class PageMainTorneosServicio
    {
        private readonly Contexto _context;

        public PageMainTorneosServicio(Contexto context)
        {
            _context = context;
        }

        public async Task<List<Torneo>> ObtenerTorneos()
        {
            var torneos = new List<Torneo>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_torneos()", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text; // Cambiar a Text para una consulta de función

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var torneo = new Torneo
                                {
                                    IdTorneo = reader.GetInt32(reader.GetOrdinal("id_torneo")),
                                    //ImgTorneo = reader.GetString(reader.GetOrdinal("IMG_torneo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Categoria = reader.GetString(reader.GetOrdinal("categoria")),
                                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    FechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin"))
                                };
                                torneos.Add(torneo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return torneos;
        }


        public async Task<List<Torneo>> ObtenerTorneoPorDeporte(int? idDeporte)
        {
            var torneos = new List<Torneo>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_torneos_por_deporte(@deporteid)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text; // Cambiar a Text para consulta de función
                        command.Parameters.AddWithValue("deporteid", idDeporte.HasValue ? (object)idDeporte.Value : DBNull.Value);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var torneo = new Torneo
                                {
                                    IdTorneo = reader.GetInt32(reader.GetOrdinal("id_torneo")),
                                    //ImgTorneo = reader.GetString(reader.GetOrdinal("IMG_torneo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Categoria = reader.GetString(reader.GetOrdinal("categoria")),
                                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    FechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin"))
                                };
                                torneos.Add(torneo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return torneos;
        }

        public async Task<List<Noticia>> ObtenerAllNoticias()
        {
            var noticias = new List<Noticia>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_all_noticias()", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text; // Cambiar a Text para una consulta de función

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                noticias.Add(new Noticia
                                {
                                    IdNoticia = reader.GetInt32(reader.GetOrdinal("id_noticia")),
                                    Titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    Contenido = reader.IsDBNull(reader.GetOrdinal("contenido")) ? null : reader.GetString(reader.GetOrdinal("contenido")),
                                    FechaPublicacion = reader.GetDateTime(reader.GetOrdinal("fecha_publicacion")),
                                    IdAutor = reader.GetInt32(reader.GetOrdinal("id_autor"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return noticias;
        }

        public async Task<List<Equipo>> ObtenerEquiposPorTorneo(int idTorneo)
        {
            var equipos = new List<Equipo>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_equipos_por_torneo(@_id_torneo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@_id_torneo", idTorneo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var equipo = new Equipo
                                {
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo")),
                                    //ImgEquipo = reader.IsDBNull(reader.GetOrdinal("img_equipo")) ? null : reader.GetString(reader.GetOrdinal("img_equipo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    IdDeporte = reader.GetInt32(reader.GetOrdinal("id_deporte")),
                                    Categoria = reader.GetString(reader.GetOrdinal("categoria")),
                                    
                                };
                                equipos.Add(equipo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return equipos;
        }

        public async Task<List<Noticia>> ObtenerNoticiasPorTorneo(int idTorneo)
        {
            var noticias = new List<Noticia>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_noticias_por_torneo(@_id_torneo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@_id_torneo", idTorneo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                noticias.Add(new Noticia
                                {
                                    IdNoticia = reader.GetInt32(reader.GetOrdinal("id_noticia")),
                                    Titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    Contenido = reader.IsDBNull(reader.GetOrdinal("contenido")) ? null : reader.GetString(reader.GetOrdinal("contenido")),
                                    FechaPublicacion = reader.GetDateTime(reader.GetOrdinal("fecha_publicacion")),
                                    IdAutor = reader.GetInt32(reader.GetOrdinal("id_autor")),
                                    IdTorneo = reader.GetInt32(reader.GetOrdinal("id_torneo"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return noticias;
        }

        public async Task<List<Partido>> ObtenerPartidosPorTorneo(int idTorneo)
{
            var partidos = new List<Partido>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_partidos_por_torneo(@_id_torneo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@_id_torneo", idTorneo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                partidos.Add(new Partido
                                {
                                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                                    Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha"))),
                                    Hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("hora"))),
                                    Ubicacion = reader.GetString(reader.GetOrdinal("ubicacion")),
                                    IdEquipoLocal = reader.GetInt32(reader.GetOrdinal("id_equipo_local")),
                                    IdEquipoVisitante = reader.GetInt32(reader.GetOrdinal("id_equipo_visitante")),
                                    IdOrganizador = reader.GetInt32(reader.GetOrdinal("id_organizador")),
                                    Estado = reader.GetString(reader.GetOrdinal("estado"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return partidos;
        }

        public async Task<List<EvaluacionesArbitraje>> ObtenerResultadosPorTorneo(int idTorneo)
        {
            var evaluaciones = new List<EvaluacionesArbitraje>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_evaluaciones_por_torneo(@idTorneo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("idTorneo", idTorneo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                evaluaciones.Add(new EvaluacionesArbitraje
                                {
                                    IdEvaluacion = reader.GetInt32(reader.GetOrdinal("id_evaluacion")),
                                    IdAsignacion = reader.GetInt32(reader.GetOrdinal("id_asignacion")),
                                    IdEquipoGanador = reader.GetInt32(reader.GetOrdinal("id_equipo_ganador")),
                                    IdEquipoPerdedor = reader.GetInt32(reader.GetOrdinal("id_equipo_perdedor")),
                                    ResultadoEquipoGanador = reader.IsDBNull(reader.GetOrdinal("resultado_equipo_ganador")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("resultado_equipo_ganador")),
                                    ResultadoEquipoPerdedor = reader.IsDBNull(reader.GetOrdinal("resultado_equipo_perdedor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("resultado_equipo_perdedor")),
                                    IdArbitro = reader.GetInt32(reader.GetOrdinal("id_arbitro")),
                                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                                    Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha"))),
                                    Hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("hora"))),
                                    Ubicacion = reader.GetString(reader.GetOrdinal("ubicacion")),
                                    Estado = reader.GetString(reader.GetOrdinal("estado"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return evaluaciones;
        }

        public async Task<List<Partido>> ObtenerPartidosPorEquipo(int idEquipo)
        {
            var partidos = new List<Partido>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_partidos_por_equipo(@id_equipo)", connection))
                    {
                        command.Parameters.AddWithValue("id_equipo", idEquipo);
                        command.CommandType = System.Data.CommandType.Text; // Cambiar a Text para una consulta de función

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                partidos.Add(new Partido
                                {
                                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                                    Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha"))),
                                    Hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("hora"))),
                                    Ubicacion = reader.GetString(reader.GetOrdinal("ubicacion")),
                                    IdEquipoLocal = reader.GetInt32(reader.GetOrdinal("id_equipo_local")),
                                    IdEquipoVisitante = reader.GetInt32(reader.GetOrdinal("id_equipo_visitante")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return partidos;
        }

        public async Task<List<EvaluacionesArbitraje>> ObtenerResultadosPorEquipo(int idEquipo)
        {
            var evaluacionesPartidos = new List<EvaluacionesArbitraje>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_evaluaciones_por_equipo(@idEquipo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@idEquipo", idEquipo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var evaluacionPartido = new EvaluacionesArbitraje
                                {
                                    IdEvaluacion = reader.GetInt32(reader.GetOrdinal("id_evaluacion")),
                                    IdAsignacion = reader.GetInt32(reader.GetOrdinal("id_asignacion")),
                                    IdEquipoGanador = reader.GetInt32(reader.GetOrdinal("id_equipo_ganador")),
                                    IdEquipoPerdedor = reader.GetInt32(reader.GetOrdinal("id_equipo_perdedor")),
                                    ResultadoEquipoGanador = reader.GetInt32(reader.GetOrdinal("resultado_equipo_ganador")),
                                    ResultadoEquipoPerdedor = reader.GetInt32(reader.GetOrdinal("resultado_equipo_perdedor")),
                                    IdArbitro = reader.GetInt32(reader.GetOrdinal("id_arbitro")),
                                    IdPartido = reader.GetInt32(reader.GetOrdinal("id_partido")),
                                    Fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha"))),
                                    Hora = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("hora"))),
                                    Ubicacion = reader.GetString(reader.GetOrdinal("ubicacion")),
                                    Estado = reader.GetString(reader.GetOrdinal("estado")),
                                    IdTorneo = reader.GetInt32(reader.GetOrdinal("id_torneo")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                };
                                evaluacionesPartidos.Add(evaluacionPartido);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (puedes registrar, lanzar de nuevo, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return evaluacionesPartidos;
        }


        public async Task<List<Jugadore>> ObtenerJugadores(int idEquipo)
        {
            var jugadores = new List<Jugadore>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_jugadores_por_equipo(@id_equipo)", connection))
                    {
                        command.Parameters.AddWithValue("id_equipo", idEquipo);
                        command.CommandType = System.Data.CommandType.Text; // Cambiar a Text para una consulta de función

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                jugadores.Add(new Jugadore
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    ImgJugador = reader.IsDBNull(reader.GetOrdinal("img_jugador")) ? null : reader.GetFieldValue<byte[]>(reader.GetOrdinal("img_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Posicion = reader.IsDBNull(reader.GetOrdinal("posicion")) ? null : reader.GetString(reader.GetOrdinal("posicion")),
                                    Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                    Numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? null : reader.GetInt32(reader.GetOrdinal("numero")),
                                    Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? null : reader.GetString(reader.GetOrdinal("descripcion")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (registrarla, volver a lanzar, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return jugadores;
        }

        public async Task<List<Noticia>> ObtenerNoticiasPorEquipo(int idEquipo)
        {
            var noticias = new List<Noticia>();

            try
            {
                using (var connection = new NpgsqlConnection(_context.Conexion))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand("SELECT * FROM obtener_noticias_por_equipo(@idEquipo)", connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@idEquipo", idEquipo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var noticia = new Noticia
                                {
                                    IdNoticia = reader.GetInt32(reader.GetOrdinal("id_noticia")),
                                    Titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    Contenido = reader.IsDBNull(reader.GetOrdinal("contenido")) ? null : reader.GetString(reader.GetOrdinal("contenido")),
                                    FechaPublicacion = reader.GetDateTime(reader.GetOrdinal("fecha_publicacion")),
                                    IdAutor = reader.GetInt32(reader.GetOrdinal("id_autor"))
                                };
                                noticias.Add(noticia);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (puedes registrar, lanzar de nuevo, etc.)
                Console.WriteLine($"Error: {ex.Message}");
            }

            return noticias;
        }








    }
}
