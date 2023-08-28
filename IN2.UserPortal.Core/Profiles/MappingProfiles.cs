using AutoMapper;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Models;

namespace IN2.UserPortal.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserLoginDto>().ReverseMap();
            CreateMap<UserModel, UserDetailDto>().ReverseMap();
            CreateMap<UserModel, UserListDto>().ReverseMap();
            CreateMap<UserModel, UserRegisterDto>().ReverseMap();
            CreateMap<UserModel, ResetPasswordDto>().ReverseMap();
            CreateMap<TicketModel, TicketDto>().ReverseMap();
        }
    }
}
