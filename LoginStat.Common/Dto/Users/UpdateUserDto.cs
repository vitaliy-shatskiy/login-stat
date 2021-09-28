using System;

namespace LoginStat.Common.Dto.Users
{
    public sealed class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}