using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class MoviesRepository :IMovies
    {
        readonly List<Movies> Movies;
        public MoviesRepository()
        {
            Movies = new List<Movies>()
            {
                new Movies{
                Id = Guid.NewGuid(),
                Name = "I May destroy you",
                Description = "Arabella is a Twitter-star-turned-novelist who found fame with her debut book Chronicles of a Fed-Up Millennial and is publicly celebrated as a Millennial icon. While struggling to meet a deadline for her second book," +
                "she takes a break from work to meet up with friends on a night out in London. The following morning, she struggles to remember what happened to her," +
                "but recalls the events of the night with the help of her friends Terry (Weruche Opia) and Kwame (Paapa Essiedu).",
                Rating = 5.00f,
                RentalID =
                {
                    RentalId = Guid.NewGuid(),
                    OnRent = true
                }

            },
                new Movies{
                Id = Guid.NewGuid(),
                Name = "LoveCraft Country",
                Description = " Lovecraft Country follows Atticus Freeman as he joins up with his friend Letitia and his Uncle George to embark on a road trip across 1950s Jim Crow America in search of his missing father",
                Rating = 5.00f,
                RentalID =
                {
                    RentalId = Guid.Empty,
                    OnRent = false
                }
                }
            };
        }

        public IEnumerable<Movies> GetMovies
        {
            get 
            {
                return Movies.OrderBy(s=>s.Id);
            }
        }

        public Movies AddMovies(Movies movies)
        {
            movies.Id = Guid.NewGuid();
            Movies.Add(movies);
            return movies;
        }

        public int Delete(Guid id)
        {
            var movie = GetMovieById(id);
            if(movie != null) 
            {
                Movies.Remove(movie);
                return 0;
            }
        throw new NullReferenceException(nameof(movie));
        }

        public Movies GetMovieById(Guid id)
        {
            if(id == null) 
            {
                throw new NullReferenceException(nameof(id));
            }
            return Movies.Where(r => r.Id == id).FirstOrDefault();
        }

        public int Save()
        {
            return 0;
        }

        public Movies Update(Movies updatedMovies)
        {
            var query = Movies.Where(r => r.Id == updatedMovies.Id).FirstOrDefault();
            if (query != null) 
            {
                query.Name = updatedMovies.Name;
                query.Rating = updatedMovies.Rating;
                query.RentalID = updatedMovies.RentalID;
                query.Description = updatedMovies.Description;
            }
            return query;
        }
    }
}
