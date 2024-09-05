using Health_Guard_Assistant.services.AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.AppointmentService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<HealthcareProvider> HealthcareProviders { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Specialties
            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { SpecialtyId = 1, Name = "Cardiology" },
                new Specialty { SpecialtyId = 2, Name = "Dermatology" },
                new Specialty { SpecialtyId = 3, Name = "Neurology" }
            );

            // Seed Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "New York" },
                new Location { LocationId = 2, Name = "Los Angeles" },
                new Location { LocationId = 3, Name = "Chicago" }
            );

            // Seed Healthcare Providers
            modelBuilder.Entity<HealthcareProvider>().HasData(
                new HealthcareProvider
                {
                    ProviderId = 1,
                    Name = "Dr. Jane Doe",
                    SpecialtyId = 1, // Cardiology
                    LocationId = 1, // New York
                    Availability = "Available Today"
                },
                new HealthcareProvider
                {
                    ProviderId = 2,
                    Name = "Dr. John Smith",
                    SpecialtyId = 2, // Dermatology
                    LocationId = 2, // Los Angeles
                    Availability = "Available This Week"
                },
                new HealthcareProvider
                {
                    ProviderId = 3,
                    Name = "Dr. Emily White",
                    SpecialtyId = 3, // Neurology
                    LocationId = 3, // Chicago
                    Availability = "Available This Month"
                }
            );

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    ProviderId = 1,
                    PatientName = "Alice Johnson",
                    AppointmentDate = new DateTime(2024, 8, 30, 10, 0, 0),
                    Status = "Confirmed"
                },
                new Appointment
                {
                    AppointmentId = 2,
                    ProviderId = 2,
                    PatientName = "Bob Brown",
                    AppointmentDate = new DateTime(2024, 8, 31, 14, 0, 0),
                    Status = "Pending"
                },
                new Appointment
                {
                    AppointmentId = 3,
                    ProviderId = 3,
                    PatientName = "Charlie Davis",
                    AppointmentDate = new DateTime(2024, 9, 1, 9, 0, 0),
                    Status = "Confirmed"
                }
            );

            // Configure entity relationships and constraints if needed
            modelBuilder.Entity<HealthcareProvider>()
                .HasOne(hp => hp.Specialty)
                .WithMany(s => s.HealthcareProviders)
                .HasForeignKey(hp => hp.SpecialtyId);

            modelBuilder.Entity<HealthcareProvider>()
                .HasOne(hp => hp.Location)
                .WithMany(l => l.HealthcareProviders)
                .HasForeignKey(hp => hp.LocationId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Provider)
                .WithMany(hp => hp.Appointments)
                .HasForeignKey(a => a.ProviderId);
        }
    }

}

