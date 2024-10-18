using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Health_Guard_Assistant.services.MedicationService.Migrations
{
    /// <inheritdoc />
    public partial class InitMedicationTableAndDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteractionWarning = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "Id", "Dosage", "InteractionWarning", "Name", "Timing" },
                values: new object[,]
                {
                    { 1, "500mg", false, "Aspirin", "Twice a day" },
                    { 2, "400mg", true, "Ibuprofen", "Three times a day" },
                    { 3, "850mg", false, "Metformin", "Once a day" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medications");
        }
    }
}
