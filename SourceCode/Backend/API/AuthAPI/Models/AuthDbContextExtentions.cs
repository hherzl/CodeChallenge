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

        public static IEnumerable<UserClaim> GetUserClaimsByUserId(this AuthDbContext dbContext, string userId)
            => dbContext.UserClaims.Where(item => item.UserId == userId);

        public static void SeedInMemory(this AuthDbContext dbContext)
        {
            dbContext.Users.Add(new User { UserId = "1000", Email = "juanperez@gmail.com", Password = "password1", Active = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "1000" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Customer" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "1000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "juanperez" }
            );

            dbContext.Users.Add(new User { UserId = "2000", Email = "mariarosales@yahoo.com", Password = "password1", Active = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "2000" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Customer" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "2000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "mariarosales" }
                );

            dbContext.Users.Add(new User { UserId = "3000", Email = "carlosfdez@outlook.com", Password = "password1", Active = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "3000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "3000" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "3000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Administrator" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "3000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "carlosfdez" }
                );

            dbContext.Users.Add(new User { UserId = "4000", Email = "maritzabatres@hotmail.com", Password = "password1", Active = true });

            dbContext.UserClaims.AddRange(
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "4000", ClaimType = JwtClaimTypes.Subject, ClaimValue = "4000" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "4000", ClaimType = JwtClaimTypes.Role, ClaimValue = "Administrator" },
                new UserClaim { UserClaimId = Guid.NewGuid(), UserId = "4000", ClaimType = JwtClaimTypes.PreferredUserName, ClaimValue = "maritzabatres" }
                );

            dbContext.SaveChanges();
        }
    }
}
