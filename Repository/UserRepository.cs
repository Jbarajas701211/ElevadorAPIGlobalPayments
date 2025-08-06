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
        public async Task<User?> GetUserForEmailAsync(string correo)
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
                        return new User
                        {
                            IdUser = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                            Name = reader["Nombre"] as string,
                            Email = reader["Correo"] as string,
                            Password = reader["Password"] as string,
                            IsBlocked = reader.GetBoolean(reader.GetOrdinal("EsBloqueado"))
                        };
                    }
                }
            }
            return null;
        }

        public async Task<bool> UpdateUserBlockedAsync(User user)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"UPDATE Usuario SET
                                EsBloqueado = @EsBloqueado
                                WHERE IdUsuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EsBloqueado", user.IsBlocked);
                cmd.Parameters.AddWithValue("@IdUsuario", user.IdUser);
                await conn.OpenAsync();
                var actualizado = await cmd.ExecuteNonQueryAsync();
                return actualizado > 0;
            }
        }

        public async Task<bool> UpdateAttemptAsync(UserAttempts userAttempt)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = @"UPDATE UsuarioIntento SET
                                Intentos = @Intentos,
                                Bloqueado = @Bloqueado,
                                FechaBloqueo = @FechaBloqueo
                                WHERE UsuarioId = @UsuarioId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Intentos", userAttempt.Attemps);
                cmd.Parameters.AddWithValue("@Bloqueado", userAttempt.Blocked);
                cmd.Parameters.AddWithValue("@FechaBloqueo", userAttempt.DateBlocked);
                cmd.Parameters.AddWithValue("@UsuarioId", userAttempt.UserId);
                await conn.OpenAsync();
                var actualizado = await cmd.ExecuteNonQueryAsync();
                return actualizado > 0;
            }
        }

        public async Task<bool> CreateAttemptAsync(UserAttempts userAttempt)
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
                cmd.Parameters.AddWithValue("@UsuarioId", userAttempt.UserId);
                cmd.Parameters.AddWithValue("@Intentos", userAttempt.Attemps);
                cmd.Parameters.AddWithValue("@Bloqueado", userAttempt.Blocked);
                cmd.Parameters.AddWithValue("@FechaBloqueo", userAttempt.DateBlocked ?? DateTime.Now);
                await conn.OpenAsync();
                var updated = await cmd.ExecuteNonQueryAsync();
                return updated > 0;
            }
        }

        public async Task<bool> CreateUserAsync(User user)
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
                    cmd.Parameters.AddWithValue("@Nombre", user.Name);
                    cmd.Parameters.AddWithValue("@Correo", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@EsBloqueado", user.IsBlocked);
                    await conn.OpenAsync();
                    var saved = await cmd.ExecuteNonQueryAsync();
                    return saved > 0;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<UserAttempts?> GetAttemptsUserAsync(int userId)
        {
            using (var conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM UsuarioIntento WHERE UsuarioId = @UsuarioId";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UsuarioId", userId);

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserAttempts
                        {
                            Attemps = reader.GetInt32(reader.GetOrdinal("Intentos")),
                            Blocked = reader.GetBoolean(reader.GetOrdinal("Bloqueado")),
                            DateBlocked = reader.GetDateTime(reader.GetOrdinal("FechaBloqueo")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UsuarioId"))
                        };
                    }
                }
            }
            return null;
        }
    }
}
