using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface IProviderService
    {
        public Task<ResponseDto>? GetProviderAsync();
        public Task<ResponseDto>? GetProviderByIdAsync(int providerId);
        public Task<ResponseDto>? CreateProviderAsync(HealthcareProviderDto healthcareProviderDto);
        public Task<ResponseDto>? UpdateProviderAsync(int providerId, HealthcareProviderDto healthcareProviderDto);
        public Task<ResponseDto>? DeleteProviderAsync(int providerId);
    }
}
