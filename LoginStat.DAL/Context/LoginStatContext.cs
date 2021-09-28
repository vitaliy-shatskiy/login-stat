using LoginStat.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.DAL.Context
{
    public class LoginStatContext : DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<UserLoginAttempt> UserLoginAttempts { get; private set; }

        public LoginStatContext(DbContextOptions<LoginStatContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}