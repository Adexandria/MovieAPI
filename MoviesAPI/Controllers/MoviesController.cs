using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/Movies")]
    public class MoviesController : ControllerBase
    {
        readonly IMovies movies;
        readonly IMapper mapper;
        public MoviesController(IMovies movies, IMapper mapper)
        {
            this.movies = movies;
            this.mapper = mapper;
        }
        //To get all movies in the database
        [HttpGet]
        public ActionResult<Movies> GetMovies()
        {
            var movie = movies.GetMovies;
            return Ok(movie);
        }
        //To get individual movie
        [HttpGet("{id}",Name ="Movie")]
        public async Task<ActionResult<MoviesDTo>> GetmovieById(Guid id)
        {
            var movie = await movies.GetMovieById(id);
            if (movie != null)
            {
                var newmovie = mapper.Map<MoviesDTo>(movie);
                return Ok(newmovie);
            }
            return NotFound();
        }
        //To add an individual movie
        [HttpPost]
        public async Task<ActionResult<MoviesDTo>> AddMovies(MovieCreateDTO movie)
        {
            if (movie != null)
            {
               var addmovie = mapper.Map<Movies>(movie);
               await movies.AddMovies(addmovie);
               await movies.Save();
               var newmovie = mapper.Map<MoviesDTo>(addmovie);
               return CreatedAtRoute("Movie",new { id = addmovie.MoviesId},newmovie);
            }
            return NotFound();
        }
        // To edit or make changes to the existing movies in the database
        [HttpPut("{id}")]
        public async Task<ActionResult<MoviesDTo>> UpdateMovies(MovieUpdateDTO movie,Guid id)
        {
            var updatemovie = mapper.Map<Movies>(movie);
            var updatedmovie = await movies.Update(updatemovie, id);
            await movies.Save();
            var newmovie = mapper.Map<MoviesDTo>(updatedmovie);
            return Ok(newmovie);
        }
        //To delete existing movie in the database
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovies(Guid id) 
        {
            await movies.Delete(id);
            return NoContent();
        }
    }
}
