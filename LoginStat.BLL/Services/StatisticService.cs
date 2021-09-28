using System.Threading.Tasks;
using AutoMapper;
using LoginStat.BLL.Services.Abstract;
using LoginStat.BLL.Services.Abstract.Base;
using LoginStat.DAL.Context;
using LoginStat.DAL.Seeding;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.BLL.Services
{
    public class StatisticService: BaseService, IStatisticService
    {
        public StatisticService(LoginStatContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task ReinitializeDataAsync(int count)
        {
            _context.UserLoginAttempts.RemoveRange(_context.UserLoginAttempts);
            _context.Users.RemoveRange(_context.Users);
            var (users, userLoginAttempts) = DataSeeding.GenerateAllData(count);
            await _context.AddRangeAsync(users);
            await _context.AddRangeAsync(userLoginAttempts);
            await _context.SaveChangesAsync();
        }
    }
}