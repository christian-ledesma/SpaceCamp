using Microsoft.AspNetCore.Identity;

namespace SpaceCamp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
