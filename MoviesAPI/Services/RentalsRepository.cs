using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class RentalsRepository : IRentals
    {
        readonly List<Movies> movies;
        public IEnumerable<Movies> GetMoviesOnRental
        {
            get
            {
                return movies.Where(r => r.RentalID.OnRent == true);
            }
        }
    }
}
