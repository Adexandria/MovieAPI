using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.DTO
{
    public class RentalCreateDTO
    {
        [Required(ErrorMessage = "True/False")]
        public bool OnRent { get; set; } = false;
       
    }
}
