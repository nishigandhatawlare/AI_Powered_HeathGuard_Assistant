using Health_Guard_Assistant.services.AuthService.Service.IService;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Health_Guard_Assistant.services.AuthService.Models;

namespace Health_Guard_Assistant.services.AuthService.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendPasswordResetEmail(string email, string resetLink)
        {
            var subject = "Password Reset Request";
            var body = $"Click the link to reset your password: {resetLink}";

            try
            {
                using (var client = new SmtpClient(_emailSettings.SMTPServer, _emailSettings.Port))
                {
                    // Set credentials and SSL
                    client.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
                    client.EnableSsl = true;  // Ensure SSL is enabled

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                // Log the SMTP exception for more details
                Console.WriteLine($"SMTP Exception: {smtpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"General Exception: {ex.Message}");
                return false;
            }
        }
    }
}
