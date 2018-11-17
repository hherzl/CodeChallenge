using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace AuthAPI
{
    public class ProfileService : IProfileService
    {
        private IAuthRepository Repository;

        public ProfileService(IAuthRepository repository)
        {
            Repository = repository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();

                var user = Repository.GetUserById(subjectId);

                context.IssuedClaims = Repository.GetUserClaimsByUserId(user.UserId).Select(item => new Claim(item.ClaimType, item.ClaimValue)).ToList();

                return Task.FromResult(0);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = Repository.GetUserById(context.Subject.GetSubjectId());

            context.IsActive = user != null && user.Active == true;

            return Task.FromResult(0);
        }
    }
}
