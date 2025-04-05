using IEEE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IEEE.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users);

            builder.HasOne(u => u.Committee)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CommitteeId);
        }
    }
}
