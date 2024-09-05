using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.AppointmentService.Models
{
    public class HealthcareProvider
    {
        [Key]
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; }
        public int LocationId { get; set; }
        public string Availability { get; set; }

        // Navigation properties
        public Specialty? Specialty { get; set; }
        public Location? Location { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
