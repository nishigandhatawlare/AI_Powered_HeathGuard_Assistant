using Health_Guard_Assistant.services.MedicationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_Guard_Assistant.services.MedicationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Adherence> Adherence { get; set; }

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
                    DosageForm = "500mg",
                    Frequency = "Twice a day",
                    AdministrationRoute = "Oral",
                    SideEffects = "Nausea, Vomiting",
                    Contraindications = "Allergy to Aspirin",
                    MedicationImageUrl = "http://example.com/images/aspirin.jpg",
                    MedicationGuideUrl = "http://example.com/guides/aspirin.pdf"
                },
                new Medication
                {
                    Id = 2,
                    Name = "Ibuprofen",
                    UserId = "rachana@gmail.com",
                    DosageForm = "400mg",
                    Frequency = "Three times a day",
                    AdministrationRoute = "Oral",
                    SideEffects = "Stomach upset, Dizziness",
                    Contraindications = "Peptic ulcer, Allergy to Ibuprofen",
                    MedicationImageUrl = "http://example.com/images/ibuprofen.jpg",
                    MedicationGuideUrl = "http://example.com/guides/ibuprofen.pdf"
                },
                new Medication
                {
                    Id = 3,
                    Name = "Metformin",
                    UserId = "rachana@gmail.com",
                    DosageForm = "850mg",
                    Frequency = "Once a day",
                    AdministrationRoute = "Oral",
                    SideEffects = "Diarrhea, Stomach upset",
                    Contraindications = "Renal impairment",
                    MedicationImageUrl = "http://example.com/images/metformin.jpg",
                    MedicationGuideUrl = "http://example.com/guides/metformin.pdf"
                }
            );

            // Seed data for Prescriptions
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    Id = 1,
                    MedicationId = 1,
                    PatientId = "rachana@gmail.com",
                    DatePrescribed = DateTime.Now.AddDays(-10),
                    PrescriptionId = Guid.NewGuid().ToString(),
                    Notes = "Take with food"
                },
                new Prescription
                {
                    Id = 2,
                    MedicationId = 2,
                    PatientId = "rachana@gmail.com",
                    DatePrescribed = DateTime.Now.AddDays(-5),
                    PrescriptionId = Guid.NewGuid().ToString(),
                    Notes = "Do not exceed recommended dose"
                }
            );

            // Seed data for Adherence
            modelBuilder.Entity<Adherence>().HasData(
                new Adherence
                {
                    Id = 1,
                    PrescriptionId = 1,
                    DateReported = DateTime.Now.AddDays(-9),
                    IsAdherent = true
                },
                new Adherence
                {
                    Id = 2,
                    PrescriptionId = 2,
                    DateReported = DateTime.Now.AddDays(-4),
                    IsAdherent = false
                }
            );
        }
    }
}
