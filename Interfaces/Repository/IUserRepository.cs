using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo);
        Task<bool> ActualizarUsuarioBloquearAsync(Usuario usuario);
        Task<bool> ActualizarIntentosAsync(UserAttempts usuarioIntento);
        Task<bool> CrearIntentosAsync(UserAttempts usuarioIntento);
        Task<bool> CrearUsuarioAsync(Usuario usuario);
        Task<UserAttempts?> ObtenerIntentosUsuarioAsync(int usuarioId);
    }
}
