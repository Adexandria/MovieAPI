using System;
using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.DTO
{
    public class RentalUpdateDTO:RentalCreateDTO
    {
        [Required(ErrorMessage ="Enter Guid Key")]
        public Guid RentalsId { get; set; }
    }
}
