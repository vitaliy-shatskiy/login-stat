using LoginStat.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoginStat.DAL.Context.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.Surname)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(u => u.UserLoginAttempts)
                .WithOne(ula => ula.User)
                .HasForeignKey(ula => ula.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}