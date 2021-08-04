using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using MoviesAPI.UserModel;

namespace MoviesAPI.Services
{
    public class MovieDb :DbContext
    {
        public MovieDb(DbContextOptions<MovieDb> options):base(options)
        {

        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Rentals> Rentals { get; set; } 
        public DbSet<UserImage> UserImage { get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rentals>().HasKey(s => s.RentalsId);
        }
    }
}
