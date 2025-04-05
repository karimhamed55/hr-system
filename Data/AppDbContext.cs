using IEEE.Entities;
using Microsoft.EntityFrameworkCore;

namespace IEEE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Tasks> Tasks { get; set; }




    }
}
