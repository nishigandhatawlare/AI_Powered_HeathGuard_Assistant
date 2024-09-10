using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;

namespace Health_Guard_Assistant.Web.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IBaseService _baseService;
        public ProviderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto>? CreateProviderAsync(HealthcareProviderDto healthcareProviderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = healthcareProviderDto,
                Url = AppointmentApiBase + "/api/providersapi/"
            });
        }

        public async Task<ResponseDto>? DeleteProviderAsync(int providerId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Url = AppointmentApiBase + "/api/providersapi/"+providerId
            });
        }

        public async Task<ResponseDto>? GetProviderAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/providersapi/"
            });       
                }

        public async Task<ResponseDto>? GetProviderByIdAsync(int providerId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/providersapi/"+providerId
            });
        }

        public async Task<ResponseDto>? UpdateProviderAsync(int providerId, HealthcareProviderDto healthcareProviderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Put,
                Data = healthcareProviderDto,
                Url = AppointmentApiBase + "/api/providersapi/"+providerId
            });
        }
    }
}
