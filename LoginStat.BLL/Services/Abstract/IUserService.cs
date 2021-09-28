using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginStat.Common.Dto.Users;

namespace LoginStat.BLL.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task<UserDto> UpdateUserByIdAsync(UpdateUserDto updateUserDto);
        Task DeleteUserByIdAsync(Guid userId);
    }
}