using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoginStat.BLL.Services.Abstract;
using LoginStat.BLL.Services.Abstract.Base;
using LoginStat.Common.Dto.Statistic;
using LoginStat.Common.Enums.Statistic;
using LoginStat.DAL.Context;
using LoginStat.DAL.Seeding;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.BLL.Services
{
    public class StatisticService : BaseService, IStatisticService
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

        public async Task<IEnumerable<LoginStatisticDto>> GetLoginStatisticAsync(
            DateTime? startDate,
            DateTime? endDate,
            Metric metric,
            bool? isSuccess)
        {
            var query = _context.UserLoginAttempts.AsNoTracking();

            if (isSuccess is not null)
                query = query.Where(attempt => attempt.IsSuccess == isSuccess.Value);

            if (startDate is not null)
                query = query.Where(attempt => attempt.AttemptTime >= startDate.Value);

            if (endDate is not null)
                query = query.Where(attempt => attempt.AttemptTime <= endDate.Value);

            return metric switch
            {
                Metric.Hour => await query.GroupBy(attempt => new {attempt.AttemptTime.Date, attempt.AttemptTime.Hour})
                    .OrderBy(attempts => attempts.Key.Date)
                    .ThenBy(attempts => attempts.Key.Hour)
                    .Select(g => new LoginStatisticDto
                    {
                        Period = $"{g.Key.Date:yyyy-MM-dd} {g.Key.Hour}:00", Value = g.Count()
                    })
                    .ToListAsync(),
                Metric.Month => await query
                    .GroupBy(attempt => new {attempt.AttemptTime.Year, attempt.AttemptTime.Month})
                    .OrderBy(attempts => attempts.Key.Year)
                    .ThenBy(attempts => attempts.Key.Month)
                    .Select(g => new LoginStatisticDto
                    {
                        Period = $"{DateTimeFormatInfo.InvariantInfo.GetMonthName(g.Key.Month)}, {g.Key.Year}",
                        Value = g.Count()
                    })
                    .ToListAsync(),
                Metric.Quarter => await query
                    .GroupBy(attempt => new
                        {attempt.AttemptTime.Year, AttemptTimeMonth = (attempt.AttemptTime.Month - 1) / 3})
                    .OrderBy(attempts => attempts.Key.Year)
                    .ThenBy(attempts => attempts.Key.AttemptTimeMonth)
                    .Select(g => new LoginStatisticDto
                    {
                        Period = $"{g.Key.Year} Q{g.Key.AttemptTimeMonth + 1}",
                        Value = g.Count()
                    })
                    .ToListAsync(),
                Metric.Year => await query.GroupBy(attempt => attempt.AttemptTime.Year)
                    .OrderBy(attempts => attempts.Key)
                    .Select(g => new LoginStatisticDto {Period = $"{g.Key}", Value = g.Count()})
                    .ToListAsync(),
                _ => throw new ArgumentOutOfRangeException(nameof(metric), metric, null)
            };
        }
    }
}