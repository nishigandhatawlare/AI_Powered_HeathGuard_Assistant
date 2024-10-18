﻿// <auto-generated />
using System;
using Health_Guard_Assistant.services.MedicationService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Health_Guard_Assistant.services.MedicationService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Adherence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAdherent")
                        .HasColumnType("bit");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("Adherence");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateReported = new DateTime(2024, 10, 9, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1186),
                            IsAdherent = true,
                            PrescriptionId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateReported = new DateTime(2024, 10, 14, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1190),
                            IsAdherent = false,
                            PrescriptionId = 2
                        });
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdministrationRoute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraindications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DosageForm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MedicationGuideUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicationImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SideEffects")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medications");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdministrationRoute = "Oral",
                            Contraindications = "Allergy to Aspirin",
                            DosageForm = "500mg",
                            Frequency = "Twice a day",
                            MedicationGuideUrl = "http://example.com/guides/aspirin.pdf",
                            MedicationImageUrl = "http://example.com/images/aspirin.jpg",
                            Name = "Aspirin",
                            SideEffects = "Nausea, Vomiting",
                            UserId = "rachana@gmail.com"
                        },
                        new
                        {
                            Id = 2,
                            AdministrationRoute = "Oral",
                            Contraindications = "Peptic ulcer, Allergy to Ibuprofen",
                            DosageForm = "400mg",
                            Frequency = "Three times a day",
                            MedicationGuideUrl = "http://example.com/guides/ibuprofen.pdf",
                            MedicationImageUrl = "http://example.com/images/ibuprofen.jpg",
                            Name = "Ibuprofen",
                            SideEffects = "Stomach upset, Dizziness",
                            UserId = "rachana@gmail.com"
                        },
                        new
                        {
                            Id = 3,
                            AdministrationRoute = "Oral",
                            Contraindications = "Renal impairment",
                            DosageForm = "850mg",
                            Frequency = "Once a day",
                            MedicationGuideUrl = "http://example.com/guides/metformin.pdf",
                            MedicationImageUrl = "http://example.com/images/metformin.jpg",
                            Name = "Metformin",
                            SideEffects = "Diarrhea, Stomach upset",
                            UserId = "rachana@gmail.com"
                        });
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatePrescribed")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrescriptionId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MedicationId");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DatePrescribed = new DateTime(2024, 10, 8, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1018),
                            MedicationId = 1,
                            Notes = "Take with food",
                            PatientId = "rachana@gmail.com",
                            PrescriptionId = "09604141-3e43-4ae5-9c74-7e914b3dcb17"
                        },
                        new
                        {
                            Id = 2,
                            DatePrescribed = new DateTime(2024, 10, 13, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1120),
                            MedicationId = 2,
                            Notes = "Do not exceed recommended dose",
                            PatientId = "rachana@gmail.com",
                            PrescriptionId = "992950a2-3830-4900-8116-3d4bd400889f"
                        });
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Adherence", b =>
                {
                    b.HasOne("Health_Guard_Assistant.services.MedicationService.Models.Prescription", "Prescription")
                        .WithMany("AdherenceRecords")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Prescription", b =>
                {
                    b.HasOne("Health_Guard_Assistant.services.MedicationService.Models.Medication", "Medication")
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Medication", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Health_Guard_Assistant.services.MedicationService.Models.Prescription", b =>
                {
                    b.Navigation("AdherenceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
