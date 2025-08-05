using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Utility
{
    public interface IUtility
    {
        string encriptarSHA256(string texto);
        RespuestaAutenticacionDTO GenerarJWT(Usuario usuario);
    }
}
