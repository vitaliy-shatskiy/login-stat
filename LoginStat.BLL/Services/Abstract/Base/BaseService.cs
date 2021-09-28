using AutoMapper;
using LoginStat.DAL.Context;

namespace LoginStat.BLL.Services.Abstract.Base
{
    public abstract class BaseService
    {
        private protected readonly LoginStatContext _context;
        private protected readonly IMapper _mapper;

        protected BaseService(LoginStatContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}