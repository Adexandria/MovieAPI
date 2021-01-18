using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
  public interface IUser
    {
        IEnumerable<Users> GetUsers { get; }
        Users GetUserById(Guid id);
        Users Add(Users users);
        int Delete(Guid Id);
        int Save();
        Users Update(Users updatedUser);
    }
}
