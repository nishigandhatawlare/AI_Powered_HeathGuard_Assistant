namespace Health_Guard_Assistant.services.ProfileService.Models.Dto
{
    public class UserProfileUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string MedicalHistory { get; set; } // Specific for patients
        public string CurrentMedications { get; set; } // Specific for patients
        public string Specialization { get; set; } // Specific for doctors
        public string MedicalLicenseNumber { get; set; } // Specific for doctors
    }
}
