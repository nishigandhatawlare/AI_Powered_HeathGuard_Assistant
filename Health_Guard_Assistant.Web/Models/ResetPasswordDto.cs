using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.Web.Models
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }       // Maps to "Email Address" input
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    // New password input
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } // Confirmation of the new password
        [Required]
        public string Token { get; set; }          // Reset token (typically received from the reset link)

    }
}
