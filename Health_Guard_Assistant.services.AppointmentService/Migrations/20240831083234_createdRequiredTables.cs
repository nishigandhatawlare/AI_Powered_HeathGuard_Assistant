using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Health_Guard_Assistant.services.AppointmentService.Migrations
{
    /// <inheritdoc />
    public partial class createdRequiredTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareProviders",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareProviders", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_HealthcareProviders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthcareProviders_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_HealthcareProviders_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "HealthcareProviders",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "New York" },
                    { 2, "Los Angeles" },
                    { 3, "Chicago" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtyId", "Name" },
                values: new object[,]
                {
                    { 1, "Cardiology" },
                    { 2, "Dermatology" },
                    { 3, "Neurology" }
                });

            migrationBuilder.InsertData(
                table: "HealthcareProviders",
                columns: new[] { "ProviderId", "Availability", "LocationId", "Name", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, "Available Today", 1, "Dr. Jane Doe", 1 },
                    { 2, "Available This Week", 2, "Dr. John Smith", 2 },
                    { 3, "Available This Month", 3, "Dr. Emily White", 3 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "PatientName", "ProviderId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 30, 10, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", 1, "Confirmed" },
                    { 2, new DateTime(2024, 8, 31, 14, 0, 0, 0, DateTimeKind.Unspecified), "Bob Brown", 2, "Pending" },
                    { 3, new DateTime(2024, 9, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Charlie Davis", 3, "Confirmed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareProviders_LocationId",
                table: "HealthcareProviders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareProviders_SpecialtyId",
                table: "HealthcareProviders",
                column: "SpecialtyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "HealthcareProviders");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
