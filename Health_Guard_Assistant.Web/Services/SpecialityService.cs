using Health_Guard_Assistant.Web.Models;
using Health_Guard_Assistant.Web.Services.IServices;
using static Health_Guard_Assistant.Web.Utility.SD;

namespace Health_Guard_Assistant.Web.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IBaseService _baseService;
        public SpecialityService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto>? CreateSpecialityAsync(SpecialtyDto specialtyDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Post,
                Data = specialtyDto,
                Url = AppointmentApiBase + "/api/specialtiesapi/"
            });
        }

        public async Task<ResponseDto>? DeleteSpecialityAsync(int specialityId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Delete,
                Url = AppointmentApiBase + "/api/specialtiesapi/"+specialityId
            });
        }

        public async Task<ResponseDto>? GetSpecialityAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/specialtiesapi/"
            });
        }

        public async Task<ResponseDto>? GetSpecialityByIdAsync(int specialityId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Get,
                Url = AppointmentApiBase + "/api/specialtiesapi/"+specialityId
            });
        }

        public async Task<ResponseDto>? UpdateSpecialityAsync(int specialityId, SpecialtyDto specialtyDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.Put,
                Data = specialtyDto,
                Url = AppointmentApiBase + "/api/specialtiesapi/"+ specialityId
            });
        }
    }
}
