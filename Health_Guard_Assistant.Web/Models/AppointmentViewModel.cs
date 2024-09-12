namespace Health_Guard_Assistant.Web.Models
{
    public class AppointmentViewModel
    {
        public List<AppointmentDto> Appointments { get; set; }
        public List<SpecialtyDto> Specialties { get; set; }
        public List<LocationDto> Locations { get; set; }
        public List<HealthcareProviderDto> Providers { get; set; }


    }
}
