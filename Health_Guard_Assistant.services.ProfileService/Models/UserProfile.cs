using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.services.ProfileService.Models
{
    public class UserProfile
    {
        public int Id { get; set; } // Primary key
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string ContactNumber { get; set; }
        public string PasswordHash { get; set; } // Store hashed password
        // Add other fields as necessary
    }

}
