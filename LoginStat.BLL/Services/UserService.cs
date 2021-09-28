using AutoMapper;
using LoginStat.BLL.Services.Abstract;
using LoginStat.BLL.Services.Abstract.Base;
using LoginStat.DAL.Context;

namespace LoginStat.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(LoginStatContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}