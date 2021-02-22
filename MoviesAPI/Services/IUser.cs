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
        Task<Users> GetUserById(Guid id);
        Task<Users> Add(Users users);
        Task<int> Delete(Guid Id);
        Task<int> Save();
        Users Update(Users updatedUser);
    }
}
