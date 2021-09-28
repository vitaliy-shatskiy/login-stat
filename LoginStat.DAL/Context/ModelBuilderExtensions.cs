using LoginStat.DAL.Context.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LoginStat.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>().HasData();
        }
    }
}