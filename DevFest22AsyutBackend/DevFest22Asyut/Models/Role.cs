using Microsoft.AspNetCore.Identity;

namespace DevFest22Asyut.Models
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new HashSet<IdentityUserRole<int>>();
        }
        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }
    }
}
