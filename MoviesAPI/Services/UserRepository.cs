using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class UserRepository :IUser
    {
        readonly List<Users> Users;
        public UserRepository()
        {
            Users = new List<Users>()
            {
                new Users() { Id = Guid.Parse("e9b8b2ba-75e0-417c-ab5e-40b6cbae50c4"), Username = "Addie"},
                new Users() {Id = Guid.Parse("5e2754ba-0778-4bf3-9146-962fb74ac546"),Username="Blankie"}
            };
        }

        public IEnumerable<Users> GetUsers
        {
            get 
            {
                return Users.OrderBy(r => r.Id);
            }
        }

        public Users Add(Users users)
        {
            users.Id = Guid.NewGuid();
            Users.Add(users);
            return users;
        }

        public int Delete(Guid Id)
        {
            var query = GetUserById(Id);
            if(query != null) 
            {
                Users.Remove(query);
                return 0;
            }
            throw new NullReferenceException(nameof(query));

        }

        public Users GetUserById(Guid id)
        {
            if (id == null)
            {
                throw new NullReferenceException(nameof(id));
            }
            return Users.Where(r => r.Id == id).FirstOrDefault();
        }

        public int Save()
        {
            return 0;
        }

        public Users Update(Users updatedUser)
        {
            var query = GetUserById(updatedUser.Id);
            if(query != null) 
            {
                query.Username = updatedUser.Username;
            }
            return query;
        }
    }
}
