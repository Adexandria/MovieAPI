using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Model
{
    public class Movies
    { 
        [Key]
        public Guid MoviesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        [ForeignKey("Users")]
        public string UserId { get; set; }
        
    }
}
