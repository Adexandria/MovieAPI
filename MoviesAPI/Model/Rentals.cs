using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
