using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTO
{
    public class RentalDTO
    {
        public bool OnRent { get; set; }
        public MoviesDTo Movies { get; set; }
    }
}
