using Health_Guard_Assistant.services.MedicationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.MedicationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Medications
            modelBuilder.Entity<Medication>().HasData(
                new Medication
                {
                    Id = 1,
                    Name = "Aspirin",
                    UserId = "rachana@gmail.com",
                    Dosage = "500mg",
                    Timing = "Twice a day",
                    InteractionWarning = false
                },
                new Medication
                {
                    Id = 2,
                    Name = "Ibuprofen",
                    UserId = "rachana@gmail.com",
                    Dosage = "400mg",
                    Timing = "Three times a day",
                    InteractionWarning = true
                },
                new Medication
                {
                    Id = 3,
                    Name = "Metformin",
                    UserId = "rachana@gmail.com",
                    Dosage = "850mg",
                    Timing = "Once a day",
                    InteractionWarning = false
                }
            );
        }
    }
}
