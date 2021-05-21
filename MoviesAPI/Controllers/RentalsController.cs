using System;
using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using MoviesAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/{username}/rentals")]
    [Authorize]
    public class RentalsController : ControllerBase
    {
        readonly IRentals rentals;
        readonly IMovies movies;
        readonly IMapper mapper;
       
        public RentalsController(IRentals rentals,IMapper mapper, IMovies movies)
        {
            this.rentals = rentals;
            this.mapper = mapper;
            this.movies = movies;
        }

        //Gets the movies on rental
        [HttpGet]
        public ActionResult<RentalDTO> GetRentals() 
        { 
            var rental = rentals.GetRentals;
            var onrent = mapper.Map<IEnumerable<RentalDTO>>(rental);
            return Ok(onrent);
        }

        //Get an Individual movie on rental 
        [HttpGet("{id}",Name ="rental")]
        public async Task<ActionResult<RentalDTO>> Rental(Guid id) 
        {
            var onrent = await rentals.GetRental(id);
            if(onrent != null) 
            {
                var rental = mapper.Map<RentalDTO>(onrent);
                return Ok(rental);
            }
            return NotFound();
        }

        // Editing an already existing rental movie
        // and if it doesn't exist
        // the function will add the rental movie to the database
        [HttpPut("{id}")]
        public  async Task<ActionResult<RentalDTO>> UpdateRental(RentalCreateDTO rental,Guid id,string username) 
        {
                var movie = await movies.GetMovieById(id,username);
                var movierental = mapper.Map<Rentals>(movie);
                movierental.OnRent = rental.OnRent;
                var newrent =  await rentals.Update(movierental,id);
                var rent = mapper.Map<RentalDTO>(newrent);
                return Ok(rent);
  
        }

        //The funtion deletes an existing rental movie
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRentals(Guid id) 
        {
            await rentals.Delete(id);
            return Ok();
        }
    }
}
