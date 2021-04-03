using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class RentalUpdateDTO:RentalCreateDTO
    {
        [Required(ErrorMessage ="Enter Guid Key")]
        public Guid RentalsId { get; set; }
    }
}
