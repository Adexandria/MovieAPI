using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Profiles
{
    public class RentalProfile :Profile
    {
        public RentalProfile()
        {
            CreateMap<Rentals, RentalDTO>()
                .ForMember(dest => dest.OnRent, opt => opt.MapFrom(s => s.OnRent))
                .ForPath(dest => dest.Movies.Name, opt => opt.MapFrom(s => s.Movies.Name))
                .ForPath(dest => dest.Movies.Description, opt => opt.MapFrom(s => s.Movies.Description))
                .ForPath(dest => dest.Movies.Rating, opt => opt.MapFrom(s => s.Movies.Rating));

            CreateMap<RentalCreateDTO, Rentals>()
               .ForMember(dest => dest.OnRent, opt => opt.MapFrom(s => s.OnRent));
             

        }
    }
}
