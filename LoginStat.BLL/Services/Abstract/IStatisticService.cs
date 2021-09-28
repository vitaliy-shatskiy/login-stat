using System;
using System.Threading.Tasks;

namespace LoginStat.BLL.Services.Abstract
{
    public interface IStatisticService
    {
        Task ReinitializeDataAsync(int count);
    }
}