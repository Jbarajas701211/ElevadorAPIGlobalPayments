using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public required string? Name { get; set; }
        public required string? Email { get; set; }
        public required string? Password { get; set; }
        public required bool IsBlocked { get; set; }
    }
}
