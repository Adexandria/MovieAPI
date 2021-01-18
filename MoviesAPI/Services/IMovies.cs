using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IMovies
    {
        IEnumerable<Movies> GetMovies { get; }
        Movies GetMovieById(Guid id);
        Movies AddMovies(Movies movies);
        int Delete(Guid id);
        Movies Update(Movies updatedMovies);
        int Save();
    }
}
