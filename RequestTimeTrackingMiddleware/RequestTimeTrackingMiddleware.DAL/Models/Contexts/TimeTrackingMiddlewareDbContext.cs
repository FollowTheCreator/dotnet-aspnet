using Microsoft.EntityFrameworkCore;

namespace RequestTimeTrackingMiddleware.DAL.Models.Contexts
{
    public partial class TimeTrackingMiddlewareDbContext : DbContext
    {
        public TimeTrackingMiddlewareDbContext()
        {
        }

        public TimeTrackingMiddlewareDbContext(DbContextOptions<TimeTrackingMiddlewareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);
            });
        }
    }
}
