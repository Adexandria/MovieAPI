using System;
using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.DTO
{
    public class UserUpdateDTO : UserCreateDTO
    {
        [Required(ErrorMessage ="Enter Guid Key")]
        public Guid Id { get; set; }
    }
}
