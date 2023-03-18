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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }

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

            builder.Entity<Comment>()
                .HasOne(x => x.Activity)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(x =>
            {
                x.HasKey(y => new { y.ObserverId, y.TargetId });
                x.HasOne(y => y.Observer)
                    .WithMany(y => y.Followings)
                    .HasForeignKey(y => y.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);
                x.HasOne(y => y.Target)
                    .WithMany(y => y.Followers)
                    .HasForeignKey(y => y.TargetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
