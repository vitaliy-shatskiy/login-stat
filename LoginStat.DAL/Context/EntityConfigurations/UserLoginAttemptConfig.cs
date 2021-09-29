using LoginStat.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoginStat.DAL.Context.EntityConfigurations
{
    public class UserLoginAttemptConfig : IEntityTypeConfiguration<UserLoginAttempt>
    {
        public void Configure(EntityTypeBuilder<UserLoginAttempt> builder)
        {
            builder.Property(attempt => attempt.AttemptTime)
                .IsRequired();

            builder.Property(attempt => attempt.IsSuccess)
                .IsRequired();
        }
    }
}