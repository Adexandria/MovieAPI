using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.UserModel
{
    public class SignUpModel
    {
        [Required(ErrorMessage ="Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password not the same")]
        public string RetypePassword { get; set; }
       
    }
}
