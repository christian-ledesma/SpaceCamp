using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SpaceCamp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public ICollection<ActivityAttendee> Activities { get; set; }
    }
}
