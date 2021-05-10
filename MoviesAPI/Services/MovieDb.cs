using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class MovieDb :DbContext
    {
        public MovieDb(DbContextOptions<MovieDb> options):base(options)
        {

        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Rentals> Rentals { get; set; } 
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rentals>().HasKey(s => s.RentalsId);
        }
    }
}
