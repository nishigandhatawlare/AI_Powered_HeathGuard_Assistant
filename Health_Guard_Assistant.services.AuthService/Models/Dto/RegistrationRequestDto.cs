namespace Health_Guard_Assistant.services.AuthService.Models.Dto
{
    public class RegistrationRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
