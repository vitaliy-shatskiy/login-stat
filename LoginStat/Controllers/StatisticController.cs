using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginStat.BLL.Services.Abstract;
using LoginStat.Common.Dto.Statistic;
using LoginStat.Common.Dto.Users;
using LoginStat.Common.Enums.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace LoginStat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("reinit/{count:int}")]
        public async Task<ActionResult<UserDto>> ReinitializeData(int count)
        {
            await _statisticService.ReinitializeDataAsync(count);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginStatisticDto>>> GetLoginStatistic(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] Metric metric,
            [FromQuery] bool? isSuccess)
        {
            return Ok(await _statisticService.GetLoginStatisticAsync(startDate, endDate, metric, isSuccess));
        }
    }
}