using Microsoft.AspNetCore.Identity;

namespace CyberSecurityProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
