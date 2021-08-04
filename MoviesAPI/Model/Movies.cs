using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Model
{
    public class Movies
    { 
        [Key]
        public Guid MoviesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MovieImageURL { get; set; }
        public float Rating { get; set; }
        [ForeignKey("Users")]
        public string UserId { get; set; }
        
    }
}
