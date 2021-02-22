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
        Task<Movies> GetMovieById(Guid id);
        Task<Movies> AddMovies(Movies movies);
        Task<int> Delete(Guid id);
        Task<Movies> Update(Movies updatedMovies,Guid id);
        Task<int> Save();
    }
}
