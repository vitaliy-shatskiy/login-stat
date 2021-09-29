using LoginStat.DAL.Context.EntityConfigurations;
using LoginStat.DAL.Entities;
using LoginStat.DAL.Seeding;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        private const int EntityCount = 2000;

        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var (users, userLoginAttempts) = DataSeeding.GenerateAllData(EntityCount);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserLoginAttempt>().HasData(userLoginAttempts);
        }
    }
}