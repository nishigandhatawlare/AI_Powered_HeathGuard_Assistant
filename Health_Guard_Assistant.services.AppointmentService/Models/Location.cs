using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.AppointmentService.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<HealthcareProvider>? HealthcareProviders { get; set; }
    }
}
