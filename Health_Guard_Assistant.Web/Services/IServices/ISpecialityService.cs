using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface ISpecialityService
    {
        public Task<ResponseDto>? GetSpecialityAsync();
        public Task<ResponseDto>? GetSpecialityByIdAsync(int specialityId);
        public Task<ResponseDto>? CreateSpecialityAsync(SpecialtyDto specialtyDto);
        public Task<ResponseDto>? UpdateSpecialityAsync(int specialityId, SpecialtyDto specialtyDto);
        public Task<ResponseDto>? DeleteSpecialityAsync(int specialityId);
    }
}
