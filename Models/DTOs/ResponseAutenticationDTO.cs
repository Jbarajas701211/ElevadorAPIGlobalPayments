using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ResponseAutenticationDTO
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
