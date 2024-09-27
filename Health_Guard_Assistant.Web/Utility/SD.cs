namespace Health_Guard_Assistant.Web.Utility
{
    public class SD
    {
        public static string AppointmentApiBase { get; set; }
        public static string AuthApiBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RolePatient = "PATIENT";
        public const string RoleDoctor = "DOCTOR";

        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
