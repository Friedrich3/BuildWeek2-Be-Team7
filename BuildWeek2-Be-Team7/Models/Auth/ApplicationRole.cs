using Microsoft.AspNetCore.Identity;

namespace BuildWeek2_Be_Team7.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {


        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
