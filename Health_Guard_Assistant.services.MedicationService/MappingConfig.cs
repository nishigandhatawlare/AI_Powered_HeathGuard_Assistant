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
                // Medication mappings
                config.CreateMap<MedicationDto, Medication>()
                      .ReverseMap(); // Bidirectional mapping

                // Prescription mappings
                config.CreateMap<PrescriptionDto, Prescription>()
                      .ReverseMap(); // Bidirectional mapping

                // Adherence mappings
                config.CreateMap<AdherenceDto, Adherence>()
                      .ReverseMap(); // Bidirectional mapping
            });
            return mappingConfig;
        }
    }
}
