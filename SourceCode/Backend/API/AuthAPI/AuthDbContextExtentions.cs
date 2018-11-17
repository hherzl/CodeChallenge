using System;
using IdentityModel;

namespace AuthAPI
{
    public static class AuthDbContextExtentions
    {
        public static void SeedInMemory(this AuthDbContext dbContext)
        {
            dbContext.Users.Add(new User { UserId = "1000", Email = "juanperez@gmail.com", Password = "password1", Active = true });

            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "1000" });
            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Customer" });
            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "juanperez" });

            dbContext.SaveChanges();

            dbContext.Users.Add(new User { UserId = "2000", Email = "mariarosales@yahoo.com", Password = "password1", Active = true });

            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "2000" });
            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Customer" });
            dbContext.UserClaims.Add(new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "mariarosales" });

            dbContext.SaveChanges();
        }
    }
}
