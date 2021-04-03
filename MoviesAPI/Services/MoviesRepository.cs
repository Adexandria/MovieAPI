using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class MoviesRepository :IMovies
    {
        readonly MovieDb db;
        public MoviesRepository(MovieDb db)
        {
            this.db = db ?? throw new NullReferenceException(nameof(db));
        }

        public IEnumerable<Movies> GetMovies
        {
            get 
            {
                return db.Movies.OrderBy(s => s.MoviesId).AsNoTracking();
            }
        }


        public async Task<Movies> AddMovies(Movies movies)
        {
            movies.MoviesId = Guid.NewGuid();
            await db.Movies.AddAsync(movies);
            return movies;
        }

        public async Task<int> Delete(Guid id)
        {
            var movie = await GetMovieById(id);
            if( movie != null) 
            {
               db.Movies.Remove(movie);
                return await db.SaveChangesAsync();
            }
        throw new NullReferenceException(nameof(movie));
        }

        public async Task<Movies> GetMovieById(Guid id)
        {
            if( id == null) 
            {
                throw new NullReferenceException(nameof(id));
            }
            return await db.Movies.Where(r => r.MoviesId == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<Movies> Update(Movies updatedMovies,Guid id)
        {
            var movie = await GetMovieById(id);
            if (movie != null)
            {
                var query = db.Movies.Attach(updatedMovies);
                query.State = EntityState.Modified;
                await db.SaveChangesAsync();
                return updatedMovies;
            }
            throw new NullReferenceException(nameof(id));
        }
    }
}
