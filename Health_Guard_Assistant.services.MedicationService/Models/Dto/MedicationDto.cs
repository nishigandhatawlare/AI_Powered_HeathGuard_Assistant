using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.MedicationService.Models.Dto
{
    public class MedicationDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }  // New field to store the logged-in patient's user ID

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Timing { get; set; }

        public bool InteractionWarning { get; set; }
    }
}
