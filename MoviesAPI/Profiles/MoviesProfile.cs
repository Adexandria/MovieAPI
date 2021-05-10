using AutoMapper;
using MoviesAPI.DTO;
using MoviesAPI.Model;

namespace MoviesAPI.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movies, MoviesDTo>();

            CreateMap<MovieCreateDTO, Movies>();

            CreateMap<MovieUpdateDTO, Movies>();
             

        }
    }
}
