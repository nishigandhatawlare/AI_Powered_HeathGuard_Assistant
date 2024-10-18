using Health_Guard_Assistant.services.ProfileService.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.ProfileService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }

}

