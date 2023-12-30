using Microsoft.AspNetCore.Identity;

namespace HMS.Infrastructure.Entities.Membership
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public string? Name { get; set; }
    }
}