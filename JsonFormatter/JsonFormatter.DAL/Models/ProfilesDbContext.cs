using Microsoft.EntityFrameworkCore;

namespace JsonFormatter.DAL.Models
{
    public class ProfilesDbContext : DbContext
    {
        public ProfilesDbContext()
        {
        }

        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(150);
            });
        }
    }
}
