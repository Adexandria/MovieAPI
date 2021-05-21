using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class MovieCreateDTO
    {
        [Required(ErrorMessage ="Enter the name of the movie")]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
    }
}
