using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define key for entities

            modelBuilder.Entity<User>(builder => builder.HasKey(p => p.UserId));

            modelBuilder.Entity<UserClaim>(builder => builder.HasKey(p => p.UserClaimId));

            base.OnModelCreating(modelBuilder);
        }
    }
}
