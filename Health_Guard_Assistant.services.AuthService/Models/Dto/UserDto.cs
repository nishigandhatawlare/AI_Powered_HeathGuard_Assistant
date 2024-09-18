namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class UserDto
    {
        public string UserId { get; set; }         // User ID
        public string FirstName { get; set; }   // First Name
        public string LastName { get; set; }    // Last Name
        public string Email { get; set; }       // Email Address
        public bool RememberMe { get; set; }    // Remember Me flag
    }
}
