using System;
using System.Collections.Generic;
using LoginStat.Common.Dto.UserLoginAttempts;

namespace LoginStat.Common.Dto.Users
{
    public sealed class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ICollection<UserLoginAttemptDto>? UserLoginAttempts { get; set; }
    }
}