using System.Collections.Generic;
using System.Linq;

namespace AuthAPI
{
    public class AuthRepository : IAuthRepository
    {
        private AuthDbContext DbContext;

        public AuthRepository(AuthDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool ValidatePassword(string userName, string password)
        {
            var user = DbContext.Users.FirstOrDefault(item => item.Email == userName);

            if (user == null)
                return false;

            if (user.Password == password)
                return true;

            return false;
        }

        public User GetUserByUserName(string userName)
            => DbContext.Users.FirstOrDefault(item => item.Email == userName);

        public User GetUserById(string id)
            => DbContext.Users.FirstOrDefault(item => item.UserId == id);

        public IEnumerable<UserClaim> GetUserClaimsByUserId(string id)
            => DbContext.UserClaims.Where(item => item.UserId == id);
    }
}
