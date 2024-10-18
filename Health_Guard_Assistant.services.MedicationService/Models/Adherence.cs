using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.MedicationService.Models
{
    public class Adherence
    {
        public int Id { get; set; }

        [Required]
        public int PrescriptionId { get; set; } // Link to the prescription

        public DateTime DateReported { get; set; }

        public bool IsAdherent { get; set; } // True if the patient took the medication as prescribed

        // Navigation property
        public virtual Prescription Prescription { get; set; } // Navigation property to the prescription
    }
}
