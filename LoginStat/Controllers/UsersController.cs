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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all/{count:int}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get(int count)
        {
            return Ok(await _userService.GetUsersAsync(count));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById([NotNull] Guid id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail([NotNull] [EmailAddress] [MaxLength(25)]
            string email)
        {
            return Ok(await _userService.GetUserByEmailAsync(email));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto userDto)
        {
            return Ok(await _userService.CreateUserAsync(userDto));
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> Update([FromBody] UpdateUserDto userDTO)
        {
            return Ok(await _userService.UpdateUserByIdAsync(userDTO));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([NotNull] Guid id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return NoContent();
        }

        [HttpPost("{id:guid}/loginAttempt")]
        public async Task<ActionResult<UserDto>> Create([NotNull] Guid id, [FromQuery] bool? isSuccess)
        {
            return Ok(await _userService.CreateUserLoginAttemptAsync(id, isSuccess));
        }
    }
}