using System;
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
    [Route("api/{username}/Movies")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        readonly IMovies movies;
        readonly IMapper mapper;
        public MoviesController(IMovies movies, IMapper mapper)
        {
            this.movies = movies;
            this.mapper = mapper;
        }
      
        //The Function gets an existing movie in the database
        [HttpGet("{id}",Name ="Movie")]
        public async Task<ActionResult<MoviesDTo>> GetmovieById(Guid id,string username)
        {
            var movie = await movies.GetMovieById(id,username);
            if (movie != null)
            {
                var newmovie = mapper.Map<MoviesDTo>(movie);
                return Ok(newmovie);
            }
            return NotFound();
        }
       
        // Editing an already existing movie
        // and if it doesn't exist
        // the function will add the movie to the database
        [HttpPut()]
        public async Task<ActionResult<MoviesDTo>> UpdateMovies(MovieUpdateDTO movie,string username)
        {
            var updatemovie = mapper.Map<Movies>(movie);
            var updatedmovie = await movies.Update(updatemovie,username);
            await movies.Save();
            var newmovie = mapper.Map<MoviesDTo>(updatedmovie);
            return Ok(newmovie);
        }
        //The funtion deletes an existing movie
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovies(Guid id,string username) 
        {
            await movies.Delete(id,username);
            return NoContent();
        }
    }
}
