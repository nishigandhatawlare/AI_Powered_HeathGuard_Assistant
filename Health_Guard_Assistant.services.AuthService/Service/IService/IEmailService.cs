namespace Health_Guard_Assistant.services.AuthService.Service.IService
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmail(string email, string resetLink);

    }
}
