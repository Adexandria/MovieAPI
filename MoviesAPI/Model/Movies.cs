using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Model
{
    public class Movies
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public Rentals RentalID { get; set; }
    }
}
