namespace Health_Guard_Assistant.Web.Models
{
    public class RegistrationRequestDto
    {
        public string FirstName { get; set; }   // Maps to "First Name" input
        public string LastName { get; set; }    // Maps to "Last Name" input
        public string Email { get; set; }       // Maps to "Email Address" input
        public string PhoneNumber { get; set; } // (Optional) Not in your form, but can be retained
        public string Password { get; set; }    // Maps to "Password" input
        public string ConfirmPassword { get; set; } // Maps to "Repeat Password" input
        public string? Role { get; set; }

    }
}
