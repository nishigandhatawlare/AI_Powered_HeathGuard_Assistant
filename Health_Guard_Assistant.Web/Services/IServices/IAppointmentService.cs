using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface IAppointmentService
    {
        public Task<ResponseDto>? GetAppointmentsAsync();
        public Task<ResponseDto>? GetAppointmentAsyncById(int appointmentId);
        public Task<ResponseDto>? CreateAppointmentAsync(AppointmentDto appointmentDto);
        public Task<ResponseDto>? UpdateAppointmentAsync(int appointmentId,AppointmentDto appointmentDto);
        public Task<ResponseDto>? DeleteAppointmentAsync(int appointmentId);
        Task<ResponseDto>? GetAppointmentsByUserIdAsync(string userId);

    }
}
