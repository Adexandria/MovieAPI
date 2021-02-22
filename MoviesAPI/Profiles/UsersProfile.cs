using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;
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
            CreateMap<Users, UserDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.Username));

            CreateMap<UserCreateDTO,Users>()
                  .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.Username));
        }
    }
}
