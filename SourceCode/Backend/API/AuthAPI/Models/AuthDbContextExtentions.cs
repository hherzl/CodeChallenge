using System;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;

namespace AuthAPI.Models
{
    public static class AuthDbContextExtentions
    {
        public static bool ValidatePassword(this AuthDbContext dbContext, string userName, string password)
        {
            var user = dbContext.Users.FirstOrDefault(item => item.Email == userName);

            if (user == null)
                return false;

            if (user.Password == password)
                return true;

            return false;
        }

        public static User GetUserByUserName(this AuthDbContext dbContext, string userName)
            => dbContext.Users.FirstOrDefault(item => item.Email == userName);

        public static User GetUserById(this AuthDbContext dbContext, string id)
            => dbContext.Users.FirstOrDefault(item => item.UserId == id);

        public static IEnumerable<UserClaim> GetUserClaimsByUserId(this AuthDbContext dbContext, string id)
            => dbContext.UserClaims.Where(item => item.UserId == id);

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
