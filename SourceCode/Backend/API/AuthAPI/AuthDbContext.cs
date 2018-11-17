using Microsoft.EntityFrameworkCore;

namespace AuthAPI
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
            modelBuilder.Entity<User>(builder => builder.HasKey(p => p.UserId));

            modelBuilder.Entity<UserClaim>(builder => builder.HasKey(p => new { p.UserClaimId }));

            base.OnModelCreating(modelBuilder);
        }
    }
}
