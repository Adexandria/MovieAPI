using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage ="Enter username")]
        public string Username { get; set; }
    }
}
