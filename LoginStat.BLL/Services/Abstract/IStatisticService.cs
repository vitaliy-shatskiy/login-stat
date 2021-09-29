using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginStat.Common.Dto.Statistic;
using LoginStat.Common.Enums.Statistic;

namespace LoginStat.BLL.Services.Abstract
{
    public interface IStatisticService
    {
        Task ReinitializeDataAsync(int count);

        Task<IEnumerable<LoginStatisticDto>> GetLoginStatisticAsync(
            DateTime? startDate,
            DateTime? endDate,
            Metric metric,
            bool? isSuccess);
    }
}