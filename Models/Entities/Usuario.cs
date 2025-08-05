using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public required string? Nombre { get; set; }
        public required string? Correo { get; set; }
        public required string? Clave { get; set; }
        public required bool EsBloqueado { get; set; }
    }
}
