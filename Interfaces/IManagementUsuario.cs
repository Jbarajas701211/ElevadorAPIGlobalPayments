using Models;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IManagementUsuario
    {
        Task<ApiResponse<RespuestaAutenticacionDTO>> RegistroUsuario(Usuario usuario);

        Task<ApiResponse<RespuestaAutenticacionDTO>> Login(LoginDTO loginDTO);
    }
}
