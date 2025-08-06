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
        public int UserId { get; set; }
        public int Attemps { get; set; }
        public bool Blocked { get; set; }
        public DateTime? DateBlocked { get; set; }
        public User User { get; set; }
    }
}
