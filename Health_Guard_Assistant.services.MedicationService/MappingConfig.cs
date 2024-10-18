using AutoMapper;
using Health_Guard_Assistant.services.MedicationService.Models;
using Health_Guard_Assistant.services.MedicationService.Models.Dto;

namespace Health_Guard_Assistant.services.MedicationService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MedicationDto, Medication>();
                config.CreateMap<Medication, MedicationDto>();
            });
            return mappingConfig;
        }
    }
}
