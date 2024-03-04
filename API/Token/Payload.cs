using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Token
{
    public class Payload
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}