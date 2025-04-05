using IEEE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IEEE.Configurations
{
    public class CommitteeConfigurations : IEntityTypeConfiguration<Committee>
    {
        public void Configure(EntityTypeBuilder<Committee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Head)
                .WithOne(h => h.Committee)
                .HasForeignKey<User>(u => u.CommitteeId);

            builder.HasMany(c => c.Users)
                .WithOne(u => u.Committee)
                .HasForeignKey(u => u.CommitteeId);
        }
    }
}
