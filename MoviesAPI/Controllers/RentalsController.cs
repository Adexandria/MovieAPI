using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using MoviesAPI.Services;


namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/rentals")]
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
        //To get all Rental Movies
        [HttpGet]
        public ActionResult<RentalDTO> GetRentals() 
        { 
            var rental = rentals.GetRentals;
            var onrent = mapper.Map<IEnumerable<RentalDTO>>(rental);
            return Ok(rental);
        }
        //To get an Individual rental movie
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
        //To Add or update an existing rental movie
        [HttpPut("{id}")]
        public  async Task<ActionResult<RentalDTO>> UpdateRental(RentalCreateDTO rental,Guid id) 
        {
                var movie = await movies.GetMovieById(id);
                var movierental = mapper.Map<Rentals>(movie);
                movierental.OnRent = rental.OnRent;
                var newrent =  await rentals.Update(movierental,id);
                var rent = mapper.Map<RentalDTO>(newrent);
                return Ok(rent);
  
        }
        //To delete an existing rental movie
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRentals(Guid id) 
        {
            await rentals.Delete(id);
            return Ok();
        }
    }
}
