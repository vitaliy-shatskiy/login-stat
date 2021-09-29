using AutoMapper;
using LoginStat.Common.Dto.UserLoginAttempts;
using LoginStat.DAL.Entities;

namespace LoginStat.BLL.MappingProfiles
{
    public class UserLoginAttemptProfile : Profile
    {
        public UserLoginAttemptProfile()
        {
            CreateMap<UserLoginAttemptDto, UserLoginAttempt>();
            CreateMap<UserLoginAttempt, UserLoginAttemptDto>();
        }
    }
}