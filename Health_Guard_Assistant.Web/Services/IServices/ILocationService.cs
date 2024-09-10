using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface ILocationService
    {
        public Task<ResponseDto>? GetLocationsAsync();
        public Task<ResponseDto>? GetLocatuionByIdAsync(int locationId);
        public Task<ResponseDto>? CreateLocationAsync(LocationDto locationDto);
        public Task<ResponseDto>? UpdateLocationAsync(int locationId, LocationDto locationDto);
        public Task<ResponseDto>? DeleteLocationAsync(int locationId);
    }
}
