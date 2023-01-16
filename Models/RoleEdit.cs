using Microsoft.AspNetCore.Identity;

namespace Merca_Darius_ExamenNew.Models
{
    public class RoleEdit
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<IdentityUser>? Members { get; set; }
        public IEnumerable<IdentityUser>? NonMembers { get; set; }

    }
}
