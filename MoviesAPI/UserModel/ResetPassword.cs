using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.UserModel
{
    public class ResetPassword
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
