using Microsoft.EntityFrameworkCore;
using SpaceCamp.Domain.Entities;

namespace SpaceCamp.Persistence.Data
{
    public class SpaceCampContext : DbContext
    {
        public SpaceCampContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
    }
}
