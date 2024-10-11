using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.Web.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; }       // Maps to "Email Address" input

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }    // Maps to "Password" input

        public bool RememberMe { get; set; }    // Maps to "Remember Me" checkbox
    }
}
