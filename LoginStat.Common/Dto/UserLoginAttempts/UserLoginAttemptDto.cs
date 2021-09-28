using System;
using LoginStat.DAL.Entities;

namespace LoginStat.Common.Dto.UserLoginAttempts
{
    public class UserLoginAttemptDto
    {
        public Guid Id { get; set; }
        public DateTime AttemptTime { get; set; }
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
    }
}