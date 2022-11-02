using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceCamp.Domain.Entities;

namespace SpaceCamp.Persistence.Data
{
    public class SpaceCampContext : IdentityDbContext<User>
    {
        public SpaceCampContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
    }
}
