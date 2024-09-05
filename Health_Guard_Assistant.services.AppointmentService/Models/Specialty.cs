using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.AppointmentService.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<HealthcareProvider>? HealthcareProviders { get; set; }
    }
}
