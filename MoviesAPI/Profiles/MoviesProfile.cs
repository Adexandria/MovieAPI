using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movies, MoviesDTo>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(s => s.Rating))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(s => s.Description));

            CreateMap<MovieCreateDTO, Movies>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(s => s.Rating))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(s => s.Description));

            CreateMap<MovieUpdateDTO, Movies>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(s => s.Name))
              .ForMember(dest => dest.Rating, opt => opt.MapFrom(s => s.Rating))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(s => s.Description));

        }
    }
}
