using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoginStat.BLL.Services.Abstract;
using LoginStat.BLL.Services.Abstract.Base;
using LoginStat.Common.Dto.Users;
using LoginStat.Common.Exceptions;
using LoginStat.DAL.Context;
using LoginStat.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(LoginStatContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(int count)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(u => u.UserLoginAttempts)
                .OrderByDescending(user => user.UserLoginAttempts.Count)
                .Take(count)
                .ToListAsync();
            if (users.Count == 0)
                throw new NotFoundException("Users");
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users
                           .AsNoTracking()
                           .Include(u => u.UserLoginAttempts)
                           .FirstOrDefaultAsync(u => u.Id == userId)
                       ?? throw new NotFoundException("User", userId.ToString());

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                           .AsNoTracking()
                           .Include(u => u.UserLoginAttempts)
                           .FirstOrDefaultAsync(u => u.Email == email)
                       ?? throw new NotFoundException("User");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                throw new InvalidOperationException("Such email is already exists");
            var user = _mapper.Map<User>(userDto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserByIdAsync(UpdateUserDto updateUserDto)
        {
            var user = await _context.Users
                           .Include(u => u.UserLoginAttempts)
                           .FirstOrDefaultAsync(u => u.Id == updateUserDto.Id)
                       ?? throw new NotFoundException("User", updateUserDto.Id.ToString());

            var updatedUser = _context.Update(_mapper.Map(updateUserDto, user));
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(updatedUser.Entity);
        }

        public async Task DeleteUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                       ?? throw new NotFoundException("User", userId.ToString());
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}