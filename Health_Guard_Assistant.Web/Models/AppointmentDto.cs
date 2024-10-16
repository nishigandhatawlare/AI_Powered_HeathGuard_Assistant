using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.Web.Models
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }

        
        public int ProviderId { get; set; }

        
        [StringLength(100)]
        public string? ProviderName { get; set; }  // Nullable ProviderName
                                                   // Add UserId property
        [Required] // Ensure this attribute is present
        [EmailAddress] // Optional: to ensure it's a valid email format
        public string UserId { get; set; } // Ensure UserId is included here 
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
