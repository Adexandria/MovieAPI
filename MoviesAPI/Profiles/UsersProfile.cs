using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using MoviesAPI.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Profiles
{
    public class UsersProfile :Profile
    {
        public UsersProfile()
        {
            CreateMap<SignUpModel, Users>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(s => s.Password));

            CreateMap<LoginModel, Users>()
               .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(s => s.Password));


        }
    }
}
