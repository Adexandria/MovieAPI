using AutoMapper;
using MoviesAPI.Model;
using MoviesAPI.UserModel;



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

            CreateMap<GithubSignUpModel, SignUpModel>()
                .ForMember(dest => dest.RetypePassword, opt => opt.MapFrom(s => s.Password));

            CreateMap<GithubLoginModel, LoginModel>();
        }
    }
}
