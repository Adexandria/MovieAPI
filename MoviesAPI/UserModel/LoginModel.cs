using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.UserModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}
