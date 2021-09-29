using System;
using System.Collections.Generic;
using LoginStat.DAL.Entities.Abstract;

namespace LoginStat.DAL.Entities
{
    public class User : BaseEntity<Guid>
    {
        public User()
        {
            UserLoginAttempts = new List<UserLoginAttempt>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public ICollection<UserLoginAttempt> UserLoginAttempts { get; }
    }
}