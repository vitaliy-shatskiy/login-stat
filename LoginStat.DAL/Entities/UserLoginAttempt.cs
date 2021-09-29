using System;
using LoginStat.DAL.Entities.Abstract;

namespace LoginStat.DAL.Entities
{
    public class UserLoginAttempt : BaseEntity<Guid>
    {
        public DateTime AttemptTime { get; set; }
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}