using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.MedicationService.Models.Dto
{
    public class AdherenceDto
    {
        public int Id { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        public DateTime DateReported { get; set; }

        public bool IsAdherent { get; set; }
    }
}
