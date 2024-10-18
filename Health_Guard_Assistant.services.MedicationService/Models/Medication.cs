﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Health_Guard_Assistant.services.MedicationService.Models
{
    public class Medication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string UserId { get; set; }  // New field to store the logged-in patient's user ID

        public string DosageForm { get; set; } // e.g., Tablet, Syrup

        [StringLength(100)]
        public string Frequency { get; set; } // e.g., Once a day

        public string AdministrationRoute { get; set; } // e.g., Oral, IV

        public string SideEffects { get; set; } // e.g., Nausea, Dizziness

        public string Contraindications { get; set; } // e.g., Allergies

        public string MedicationImageUrl { get; set; } // URL to medication image

        public string MedicationGuideUrl { get; set; } // URL to official medication guide

        // Navigation property to prescriptions
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
