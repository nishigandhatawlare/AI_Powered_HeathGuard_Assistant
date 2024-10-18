using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.MedicationService.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        [Required]
        public int MedicationId { get; set; }

        public string PatientId { get; set; } // Assuming you have a Patient model

        public DateTime DatePrescribed { get; set; }

        [Required]
        [StringLength(100)]
        public string PrescriptionId { get; set; } // Unique ID for the prescription

        public string Notes { get; set; } // Any notes from the doctor

        // Navigation properties
        public virtual Medication Medication { get; set; } // Navigation property to the medication
        public virtual ICollection<Adherence> AdherenceRecords { get; set; } = new List<Adherence>(); // Navigation property for adherence
    }
}
