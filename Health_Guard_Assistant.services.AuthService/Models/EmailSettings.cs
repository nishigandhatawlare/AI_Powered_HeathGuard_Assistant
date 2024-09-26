namespace Health_Guard_Assistant.services.AuthService.Models
{
    public class EmailSettings
    {
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseSSL { get; set; }
    }
}
