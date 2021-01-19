using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Model;
using MoviesAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/rentals")]
    public class RentalsController : ControllerBase
    {
        readonly IMovies movies;
        public RentalsController(IMovies movies)
        {
            this.movies = movies;
        }
        public ActionResult<Movies> GetRentals() 
        {
            return Ok(movies.GetMoviesOnRental);
        }
    }
}
