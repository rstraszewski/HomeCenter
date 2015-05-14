using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCenter.AuthenticationDomain
{
    public class UserDtoInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
