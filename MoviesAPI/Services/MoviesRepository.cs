using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class MoviesRepository :IMovies
    {
        readonly MovieDb db;
        readonly UserManager<Users> userManager;
        public MoviesRepository(MovieDb db, UserManager<Users> userManager)
        {
            this.db = db ?? throw new NullReferenceException(nameof(db));
            this.userManager = userManager ?? throw new NullReferenceException(nameof(userManager));
        }

        public IEnumerable<Movies> GetMovies
        {
            get 
            {
                return db.Movies.OrderBy(s => s.MoviesId).AsNoTracking();
            }
        }


        public async Task<Movies> AddMovies(Movies movies,string username)
        {
            var userId = await GetUserIdByUserName(username);
            movies.UserId = userId;
            movies.MoviesId = Guid.NewGuid();
            await db.Movies.AddAsync(movies);
            return movies;
        }

        public async Task<int> Delete(Guid id,string username)
        {
            var movie = await GetMovieById(id,username);
            if( movie != null) 
            {
                db.Movies.Remove(movie);
                return await db.SaveChangesAsync();
            }
        throw new NullReferenceException(nameof(movie));
        }

        public async Task<Movies> GetMovieById(Guid id,string username)
        {
            if( id == null) 
            {
                throw new NullReferenceException(nameof(id));
            }
            var userId = await GetUserIdByUserName(username);
            return await db.Movies.Where(r => r.MoviesId == id).Where(s=>s.UserId==userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string> GetUserIdByUserName(string username)
        {
            var currentuser = await userManager.FindByNameAsync(username);
            if (currentuser == null) throw new NullReferenceException(nameof(currentuser));
            return currentuser.Id;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<Movies> Update(Movies updatedMovie,string username)
        {
            var movie = await GetMovieById(updatedMovie.MoviesId,username);
            if (movie == null)
            {
                await AddMovies(updatedMovie, username);
                await Save();
            }
            else
            {
                updatedMovie.UserId = movie.UserId;
                db.Entry(movie).State = EntityState.Detached;
                db.Entry(updatedMovie).State = EntityState.Modified;
                await Save();
            }
           
            return await GetMovieById(updatedMovie.MoviesId, username);
        }
    }
}
