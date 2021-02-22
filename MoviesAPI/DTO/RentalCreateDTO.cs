using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class RentalCreateDTO
    {
        [Required(ErrorMessage = "True/False")]
        public bool OnRent { get; set; } = false;
       
    }
}
