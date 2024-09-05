namespace Health_Guard_Assistant.services.AppointmentService.Models.Dto
{
    public class HealthcareProviderDto
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string SpecialtyName { get; set; }
        public string LocationName { get; set; }
        public string Availability { get; set; }
    }
}
