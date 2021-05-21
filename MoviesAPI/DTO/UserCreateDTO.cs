using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.DTO
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage ="Enter username")]
        public string Username { get; set; }
    }
}
