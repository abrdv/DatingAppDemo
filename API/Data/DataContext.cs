using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLike>()
                .HasKey(l => new { l.SourceUserId, l.TargetUserId });

            modelBuilder.Entity<UserLike>()
                .HasOne(u => u.SourceUser)
                .WithMany(u => u.LikedUsers)
                .HasForeignKey(l => l.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLike>()
                .HasOne(u => u.TargetUser)
                .WithMany(u => u.LikedByUsers)
                .HasForeignKey(l => l.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
