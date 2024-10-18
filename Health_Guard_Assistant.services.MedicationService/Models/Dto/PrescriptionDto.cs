using System;
using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.MedicationService.Models.Dto
{
    public class PrescriptionDto
    {
        public int Id { get; set; }

        [Required]
        public int MedicationId { get; set; }

        public string PatientId { get; set; }

        public DateTime DatePrescribed { get; set; }

        [Required]
        [StringLength(100)]
        public string PrescriptionId { get; set; }

        public string Notes { get; set; }
    }
}
