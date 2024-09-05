using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.AppointmentService.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int ProviderId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }

        // Navigation property
        public HealthcareProvider? Provider { get; set; }
    }
}
