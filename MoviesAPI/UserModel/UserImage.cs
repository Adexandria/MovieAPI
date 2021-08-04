using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.UserModel
{
    public class UserImage
    {
        [Key]
        public Guid ImageId { get; set; }
        public string UserImageURL { get; set; }
        [ForeignKey("AspNetUsers")]
        public int UserId { get; set; }
    }
}
