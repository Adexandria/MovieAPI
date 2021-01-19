using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public MoviesController(IMovies movies)
        {
            this.movies = movies;
        }
        public ActionResult<Movies> GetMovies()
        {
            var movie = movies.GetMovies;
            return Ok(movie);
        }
        [HttpGet("{id}", Name = "Movie")]
        public ActionResult<Movies> GetmovieById(Guid id)
        {
            var movie = movies.GetMovieById(id);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<Movies> AddMovies(Movies movie)
        {
            if (movie != null)
            {
                movies.AddMovies(movie);
                movies.Save();
                return Ok(movie);
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult<Movies> UpdateMovies(Movies movie,Guid id) 
        {
            var newmovie = movies.GetMovieById(id);
            if(newmovie != null) 
            {
                var updatedmovie = movies.Update(movie);
                movies.Save();
                return Ok(updatedmovie);
            }
            return NotFound();

        }
        [HttpDelete("{id}")]
        public ActionResult DeleteMovies(Guid id) 
        {
            movies.Delete(id);
            return Ok();
        }
    }
}
