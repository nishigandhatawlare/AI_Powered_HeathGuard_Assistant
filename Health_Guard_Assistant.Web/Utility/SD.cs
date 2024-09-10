namespace Health_Guard_Assistant.Web.Utility
{
    public class SD
    {
        public static string AppointmentApiBase { get; set; }
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
