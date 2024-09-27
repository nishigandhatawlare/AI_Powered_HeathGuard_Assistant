namespace Health_Guard_Assistant.Web.Models
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }       // Maps to "Email Address" input
        public string Password { get; set; }    // New password input
        public string ConfirmPassword { get; set; } // Confirmation of the new password
        public string Token { get; set; }          // Reset token (typically received from the reset link)

    }
}
