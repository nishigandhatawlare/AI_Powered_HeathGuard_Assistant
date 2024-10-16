using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Guard_Assistant.services.AppointmentService.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdForeignKeyToAppointment1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "UserId",
                value: "9d95de32-b1be-4cc9-8144-95674b103156");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 2,
                column: "UserId",
                value: "9d95de32-b1be-4cc9-8144-95674b103156");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 3,
                column: "UserId",
                value: "9d95de32-b1be-4cc9-8144-95674b103156");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "UserId",
                value: "user123");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 2,
                column: "UserId",
                value: "user456");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 3,
                column: "UserId",
                value: "user789");
        }
    }
}
