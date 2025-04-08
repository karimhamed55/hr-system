using IEEE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IEEE.Configurations
{
    public class Users_TasksConfigurations : IEntityTypeConfiguration<Users_Tasks>
    {
        public void Configure(EntityTypeBuilder<Users_Tasks> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TaskId });

            builder.HasOne(ut => ut.User)
           .WithMany(u => u.Users_Tasks)
           .HasForeignKey(ut => ut.UserId)
           .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(ut => ut.Tasks)
           .WithMany(u => u.Users_Tasks)
           .HasForeignKey(ut => ut.TaskId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
