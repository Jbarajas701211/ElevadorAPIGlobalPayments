using Interfaces.DbContext;
using Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppDbContext _context;

        public UserRepository(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Usuario WHERE Correo = @Correo";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Correo", correo);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Usuario
                        {
                            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                            Nombre = reader["Nombre"] as string,
                            Correo = reader["Correo"] as string,
                            Clave = reader["Password"] as string,
                            EsBloqueado = reader.GetBoolean(reader.GetOrdinal("EsBloqueado"))
                        };
                    }
                }
            }
            return null;
        }

        public async Task<bool> ActualizarUsuarioBloquearAsync(Usuario usuario)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"UPDATE Usuario SET
                                EsBloqueado = @EsBloqueado
                                WHERE IdUsuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EsBloqueado", usuario.EsBloqueado);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                await conn.OpenAsync();
                var actualizado = await cmd.ExecuteNonQueryAsync();
                return actualizado > 0;
            }
        }

        public async Task<bool> ActualizarIntentosAsync(UserAttempts usuarioIntento)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"UPDATE UsuarioIntento SET
                                Intentos = @Intentos,
                                Bloqueado = @Bloqueado,
                                FechaBloqueo = @FechaBloqueo
                                WHERE UsuarioId = @UsuarioId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Intentos", usuarioIntento.Intentos);
                cmd.Parameters.AddWithValue("@Bloqueado", usuarioIntento.Bloqueado);
                cmd.Parameters.AddWithValue("@FechaBloqueo", usuarioIntento.FechaBloqueo);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioIntento.UsuarioId);
                await conn.OpenAsync();
                var actualizado = await cmd.ExecuteNonQueryAsync();
                return actualizado > 0;
            }
        }

        public async Task<bool> CrearIntentosAsync(UserAttempts usuarioIntento)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"INSERT INTO UsuarioIntento 
                                (UsuarioId, Intentos, Bloqueado, FechaBloqueo)
                                Values(
                                @UsuarioId, 
                                @Intentos,
                                @Bloqueado,
                                @FechaBloqueo)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioIntento.UsuarioId);
                cmd.Parameters.AddWithValue("@Intentos", usuarioIntento.Intentos);
                cmd.Parameters.AddWithValue("@Bloqueado", usuarioIntento.Bloqueado);
                cmd.Parameters.AddWithValue("@FechaBloqueo", usuarioIntento.FechaBloqueo ?? DateTime.Now);
                await conn.OpenAsync();
                var actualizado = await cmd.ExecuteNonQueryAsync();
                return actualizado > 0;
            }
        }

        public async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var conn = _context.CreateConnection())
                {
                    string query = @"INSERT INTO Usuario
                                (Nombre, Correo, Password, EsBloqueado)
                                Values(
                                @Nombre, 
                                @Correo,
                                @Password,
                                @EsBloqueado)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Password", usuario.Clave);
                    cmd.Parameters.AddWithValue("@EsBloqueado", usuario.EsBloqueado);
                    await conn.OpenAsync();
                    var guardado = await cmd.ExecuteNonQueryAsync();
                    return guardado > 0;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<UserAttempts?> ObtenerIntentosUsuarioAsync(int usuarioId)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM UsuarioIntento WHERE UsuarioId = @UsuarioId";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserAttempts
                        {
                            Intentos = reader.GetInt32(reader.GetOrdinal("Intentos")),
                            Bloqueado = reader.GetBoolean(reader.GetOrdinal("Bloqueado")),
                            FechaBloqueo = reader.GetDateTime(reader.GetOrdinal("FechaBloqueo")),
                            UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId"))
                        };
                    }
                }
            }
            return null;
        }
    }
}
