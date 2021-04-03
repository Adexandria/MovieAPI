using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class MovieUpdateDTO: MovieCreateDTO
    {
       [Required(ErrorMessage ="Enter Guid key")]
       public Guid MoviesId { get; set; }
    }
}
