using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using MoviesAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/rentals")]
    public class RentalsController : ControllerBase
    {
        readonly IRentals rentals;
        readonly IMapper mapper;
        public RentalsController(IRentals rentals,IMapper mapper)
        {
            this.rentals = rentals;
            this.mapper = mapper;
        }
        //To get all Rental Movies
        [HttpGet]
        public ActionResult<RentalDTO> GetRentals() 
        { 
            var rental = rentals.GetRentals;
            var onrent = mapper.Map<IEnumerable<RentalDTO>>(rental);
            return Ok(onrent);
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
        public  async Task<ActionResult<RentalDTO>> UpdateRental(Rentals rental,Guid id) 
        {
            var onrent =  await rentals.Update(rental, id);
            await rentals.Save();
            var rent = mapper.Map<RentalDTO>(onrent);
            return Ok(rent);
        }
        //To delete an existing rental movie
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRentals(Guid id) 
        {
            await rentals.Delete(id);
            return NoContent();
        }
    }
}
