using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;

namespace Health_Guard_Assistant.Web.Services
{
    public class LocationService : ILocationService
    {
        private readonly IBaseService _baseService;
        public LocationService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto>? CreateLocationAsync(LocationDto locationDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = locationDto,
                Url = AppointmentApiBase + "/api/locationsapi/"
            });
        }

        public async Task<ResponseDto>? DeleteLocationAsync(int locationId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Delete,
                Url = AppointmentApiBase + "/api/locationsapi/"+locationId
            });
        }

        public async Task<ResponseDto>? GetLocationsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/locationsapi/"
            });
        }

        public async Task<ResponseDto>? GetLocatuionByIdAsync(int locationId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/locationsapi/"+locationId
            });
        }

        public async Task<ResponseDto>? UpdateLocationAsync(int locationId, LocationDto locationDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Put,
                Data = locationDto,
                Url = AppointmentApiBase + "/api/locationsapi/"+locationId
            });
        }
    }
}
