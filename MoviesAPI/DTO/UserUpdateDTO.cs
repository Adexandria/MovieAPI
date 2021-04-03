using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class UserUpdateDTO : UserCreateDTO
    {
        [Required(ErrorMessage ="Enter Guid Key")]
        public Guid Id { get; set; }
    }
}
