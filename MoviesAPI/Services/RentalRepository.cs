using System;
using System.Linq;
using MoviesAPI.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Services
{
    public class RentalRepository : IRentals
    {
        readonly MovieDb db;
        readonly UserManager<Users> userManager;

        public RentalRepository(MovieDb db, UserManager<Users> userManager)
        {
            this.db = db ?? throw new NullReferenceException(nameof(db));
            this.userManager = userManager ?? throw new NullReferenceException(nameof(userManager));

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

        public async Task VerifyUserByUserName(string username)
        {
            var currentuser = await userManager.FindByNameAsync(username);
            if (currentuser == null) throw new NullReferenceException(nameof(currentuser));
           
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
