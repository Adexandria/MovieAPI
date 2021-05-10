using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MoviesAPI.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class RentalRepository : IRentals
    {
        readonly MovieDb db;
        readonly IMovies movies;
 
        public RentalRepository(MovieDb db, IMovies movies)
        {
            this.db = db ?? throw new NullReferenceException(nameof(db));
            this.movies = movies;
            
        }
        public IEnumerable<Rentals> GetRentals => db.Rentals.OrderBy(s=>s.RentalsId).Where(s=>s.OnRent == true).Include(s=>s.Movies).AsNoTracking();

     

        public async Task<int> Delete(Guid id)
        {
            var rental = await GetRental(id);
            if ( rental != null)
            {
                db.Rentals.Remove(rental);
                return await db.SaveChangesAsync();
            }
            throw new NullReferenceException(nameof(rental));
        }

        public async Task<Rentals> GetRental(Guid id)
        {
            if(id != null) 
            {
                return await db.Rentals.Where(s => s.RentalsId == id).Where(s => s.OnRent == true).Include(s=>s.Movies).AsNoTracking().FirstOrDefaultAsync();
            }
            throw new NullReferenceException(nameof(id));
        }

      
        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }

        public async Task<Rentals> Update(Rentals rentals, Guid id)
        {
            var rental = await GetRental(id);
            if (rental == null) 
            {
                
                await db.Rentals.AddAsync(rentals);
                await Save();
            }
            else
            {
                db.Entry(rental).State = EntityState.Detached;
                db.Entry(rentals).State = EntityState.Modified;
            }
            return await GetRental(rentals.RentalsId);
        }
    }
}
