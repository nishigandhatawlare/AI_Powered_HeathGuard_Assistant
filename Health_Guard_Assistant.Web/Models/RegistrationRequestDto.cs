using System.ComponentModel.DataAnnotations;

namespace Health_Guard_Assistant.Web.Models
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }   // Maps to "First Name" input

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }    // Maps to "Last Name" input

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; }       // Maps to "Email Address" input

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } // (Optional) Not in your form, but can be retained

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }    // Maps to "Password" input

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } // Maps to "Repeat Password" input

        public string? Role { get; set; } // Optional role field
    }
}
