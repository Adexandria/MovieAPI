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
        Task<string> GetUserIdByUserName(string username);
        Task<Movies> GetMovieById(Guid id,string username);
        Task<Movies> AddMovies(Movies movies,string username);
        Task<int> Delete(Guid id,string username);
        Task<Movies> Update(Movies updatedMovies,string username);
        Task<int> Save();
    }
}
