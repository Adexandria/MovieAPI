using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
   public interface IRentals
    {
        IEnumerable<Movies> GetMoviesOnRental { get; }

    }
}
