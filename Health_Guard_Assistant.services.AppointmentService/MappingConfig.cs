using AutoMapper;
using Health_Guard_Assistant.services.AppointmentService.Models;
using Health_Guard_Assistant.services.AppointmentService.Models.Dto;

namespace Health_Guard_Assistant.services.AppointmentService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AppointmentDto, Appointment>();
                config.CreateMap<Appointment, AppointmentDto>();

                config.CreateMap<HealthcareProviderDto, HealthcareProvider>();
                config.CreateMap<HealthcareProvider, HealthcareProviderDto>();

                config.CreateMap<LocationDto, Location>();
                config.CreateMap<Location, LocationDto>();

                config.CreateMap<SpecialtyDto, Specialty>();
                config.CreateMap<Specialty, SpecialtyDto>();

            });
            return mappingConfig;
        }
    }
}
