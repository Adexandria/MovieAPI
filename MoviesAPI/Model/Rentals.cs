using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Model
{
    public class Rentals
    {
        public Guid RentalId { get; set; }
        public bool OnRent { get; set; }
    }
}
