using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Guard_Assistant.services.AppointmentService.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdForeignKeyToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointments");
        }
    }
}
