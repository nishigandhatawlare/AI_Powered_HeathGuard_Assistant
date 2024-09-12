namespace Health_Guard_Assistant.Web.Models
{
    public class AppointmentDto
    {

        public int AppointmentId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
