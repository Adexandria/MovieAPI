using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Model
{
    public class Rentals
    {
        [ForeignKey("Movies")]
        public Guid RentalsId { get; set; }
        public bool OnRent { get; set; }
        public virtual Movies Movies { get; set; }
     
    }
}
