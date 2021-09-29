using AutoMapper;
using LoginStat.Common.Dto.Users;
using LoginStat.DAL.Entities;

namespace LoginStat.BLL.MappingProfiles
{
    public sealed class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}