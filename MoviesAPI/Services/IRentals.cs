using System;
using MoviesAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IRentals
    {
        IEnumerable<Rentals> GetRentals { get; }
        Task VerifyUserByUserName(string username);
        Task<Rentals> GetRental(Guid id);
        Task<Rentals> Update(Rentals rentals,Guid id);
        Task<int> Delete(Guid id);
        Task<int> Save();
    }
}
