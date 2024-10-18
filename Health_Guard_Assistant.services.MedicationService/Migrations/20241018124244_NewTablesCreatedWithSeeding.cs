using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Health_Guard_Assistant.services.MedicationService.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesCreatedWithSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InteractionWarning",
                table: "Medications");

            migrationBuilder.RenameColumn(
                name: "Timing",
                table: "Medications",
                newName: "SideEffects");

            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "Medications",
                newName: "MedicationImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "AdministrationRoute",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contraindications",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DosageForm",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Medications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicationGuideUrl",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePrescribed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrescriptionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adherence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdherent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adherence_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdministrationRoute", "Contraindications", "DosageForm", "Frequency", "MedicationGuideUrl", "MedicationImageUrl", "SideEffects" },
                values: new object[] { "Oral", "Allergy to Aspirin", "500mg", "Twice a day", "http://example.com/guides/aspirin.pdf", "http://example.com/images/aspirin.jpg", "Nausea, Vomiting" });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AdministrationRoute", "Contraindications", "DosageForm", "Frequency", "MedicationGuideUrl", "MedicationImageUrl", "SideEffects" },
                values: new object[] { "Oral", "Peptic ulcer, Allergy to Ibuprofen", "400mg", "Three times a day", "http://example.com/guides/ibuprofen.pdf", "http://example.com/images/ibuprofen.jpg", "Stomach upset, Dizziness" });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AdministrationRoute", "Contraindications", "DosageForm", "Frequency", "MedicationGuideUrl", "MedicationImageUrl", "SideEffects" },
                values: new object[] { "Oral", "Renal impairment", "850mg", "Once a day", "http://example.com/guides/metformin.pdf", "http://example.com/images/metformin.jpg", "Diarrhea, Stomach upset" });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "DatePrescribed", "MedicationId", "Notes", "PatientId", "PrescriptionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1018), 1, "Take with food", "rachana@gmail.com", "09604141-3e43-4ae5-9c74-7e914b3dcb17" },
                    { 2, new DateTime(2024, 10, 13, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1120), 2, "Do not exceed recommended dose", "rachana@gmail.com", "992950a2-3830-4900-8116-3d4bd400889f" }
                });

            migrationBuilder.InsertData(
                table: "Adherence",
                columns: new[] { "Id", "DateReported", "IsAdherent", "PrescriptionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1186), true, 1 },
                    { 2, new DateTime(2024, 10, 14, 18, 12, 42, 374, DateTimeKind.Local).AddTicks(1190), false, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adherence_PrescriptionId",
                table: "Adherence",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions",
                column: "MedicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adherence");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "AdministrationRoute",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Contraindications",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DosageForm",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "MedicationGuideUrl",
                table: "Medications");

            migrationBuilder.RenameColumn(
                name: "SideEffects",
                table: "Medications",
                newName: "Timing");

            migrationBuilder.RenameColumn(
                name: "MedicationImageUrl",
                table: "Medications",
                newName: "Dosage");

            migrationBuilder.AddColumn<bool>(
                name: "InteractionWarning",
                table: "Medications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Dosage", "InteractionWarning", "Timing" },
                values: new object[] { "500mg", false, "Twice a day" });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Dosage", "InteractionWarning", "Timing" },
                values: new object[] { "400mg", true, "Three times a day" });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Dosage", "InteractionWarning", "Timing" },
                values: new object[] { "850mg", false, "Once a day" });
        }
    }
}
