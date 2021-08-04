using MoviesAPI.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IUserImage 
    {
        Task<UserImage> AddUserImage(string userId,string url);
    }
}
