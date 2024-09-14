namespace Health_Guard_Assistant.services.AppointmentService.Models.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }  // Nullable ProviderName
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
