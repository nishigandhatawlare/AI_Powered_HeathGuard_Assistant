using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;

namespace Health_Guard_Assistant.Web.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IBaseService _baseService;
        public AppointmentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto>? CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = appointmentDto,
                Url = AppointmentApiBase + "/api/appointmentapi/"
            });
        }

        public async Task<ResponseDto>? DeleteAppointmentAsync(int appointmentId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Delete,
                Url = AppointmentApiBase + "/api/appointmentapi/"+appointmentId
            });
        }

        public async Task<ResponseDto>? GetAppointmentAsyncById(int appointmentId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/appointmentapi/"+appointmentId
            });
        }

        public async Task<ResponseDto>? GetAppointmentsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/appointmentapi/"
            });
        }

        public async Task<ResponseDto>? UpdateAppointmentAsync(int appointmentId, AppointmentDto appointmentDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Put,
                Data = appointmentDto,
                Url = AppointmentApiBase + "/api/appointmentapi/"+appointmentId
            });
        }
        public async Task<ResponseDto>? GetAppointmentsByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + $"/api/appointmentapi/ByUser/{userId}"
            });
        }
    }
}
