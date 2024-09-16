using Microsoft.AspNetCore.Identity;

namespace Health_Guard_Assistant.services.AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Name { get; set; }
    }
}
