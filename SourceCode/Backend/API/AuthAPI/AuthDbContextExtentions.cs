using System;
using IdentityModel;

namespace AuthAPI
{
    public static class AuthDbContextExtentions
    {
        public static void SeedInMemory(this AuthDbContext dbContext)
        {
            dbContext.Users.Add(new User
            {
                UserId = "1000",
                Email = "juanperez@gmail.com",
                Password = "password1",
                Active = true
            });

            dbContext.SaveChanges();

            dbContext.UserClaims.Add(new UserClaim
            {
                UserClaimId = Guid.NewGuid(),
                UserId = "1000",
                ClaimType = JwtClaimTypes.Subject,
                ClaimValue = "1000"
            });

            dbContext.SaveChanges();

            dbContext.UserClaims.Add(new UserClaim
            {
                UserClaimId = Guid.NewGuid(),
                UserId = "1000",
                ClaimType = JwtClaimTypes.Role,
                ClaimValue = "Customer"
            });

            dbContext.SaveChanges();
        }
    }
}
