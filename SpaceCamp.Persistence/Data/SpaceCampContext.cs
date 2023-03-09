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
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(entity =>
            {
                entity.HasKey(x => new { x.UserId, x.ActivityId });
            });

            builder.Entity<ActivityAttendee>()
                .HasOne(x => x.User)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.UserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(x => x.Activity)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.ActivityId);
        }
    }
}
