using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.Web.Models
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }

        
        public int ProviderId { get; set; }

        
        [StringLength(100)]
        public string? ProviderName { get; set; }  // Nullable ProviderName

        [Required]
        [StringLength(100)]
        public string PatientName { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
