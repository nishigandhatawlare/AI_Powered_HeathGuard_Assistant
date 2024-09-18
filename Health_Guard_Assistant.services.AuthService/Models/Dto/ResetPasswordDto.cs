namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }       // Maps to "Email Address" input
        public string Password { get; set; }    // New password input
        public string ConfirmPassword { get; set; } // Confirmation of the new password
    }
}
