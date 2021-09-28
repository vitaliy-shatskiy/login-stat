using System.Collections.Generic;
using System.Threading.Tasks;
using LoginStat.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginStat.Controllers
{
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}