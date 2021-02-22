using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Model
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }

    }
}
