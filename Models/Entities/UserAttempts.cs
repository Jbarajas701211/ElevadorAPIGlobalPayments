using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class UserAttempts
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int Intentos { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime? FechaBloqueo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
