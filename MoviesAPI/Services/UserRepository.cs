using Microsoft.EntityFrameworkCore;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class UserRepository :IUser
    {
        private readonly MovieDb db;
        public UserRepository(MovieDb db)
        {
            this.db = db ?? throw new NullReferenceException(nameof(db));
        }

        public IEnumerable<Users> GetUsers
        {
            get 
            {
                return db.Users.OrderBy(r => r.Id).AsNoTracking();
            }
        }

        public async Task<Users> Add(Users users)
        {
           users.Id = Guid.NewGuid();
           await db.Users.AddAsync(users);
           return users;
        }

        public async Task<int> Delete(Guid Id)
        {
            var query = await GetUserById(Id);
            if(query != null) 
            {
                db.Users.Remove(query);
                return await db.SaveChangesAsync();
            }
            throw new NullReferenceException(nameof(query));

        }

        public async Task<Users> GetUserById(Guid id)
        {
            if (id == null)
            {
                throw new NullReferenceException(nameof(id));
            }
            return await db.Users.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }

        public Users Update(Users updatedUser)
        {
            var query = db.Users.Attach(updatedUser);
            query.State = EntityState.Modified;
            return updatedUser;
        }
    }
}
