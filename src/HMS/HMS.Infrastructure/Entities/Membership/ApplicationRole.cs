using Microsoft.AspNetCore.Identity;

namespace HMS.Infrastructure.Entities.Membership
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        { 

        }
    }
}