using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using LoginStat.BLL.Services.Abstract;
using LoginStat.Common.Dto.Users;
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
        
    }
}