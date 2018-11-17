using System.Collections.Generic;

namespace AuthAPI
{
    public interface IAuthRepository
    {
        bool ValidatePassword(string userName, string password);

        User GetUserByUserName(string userName);

        User GetUserById(string id);

        IEnumerable<UserClaim> GetUserClaimsByUserId(string id);
    }
}
