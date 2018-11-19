using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthAPI.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace AuthAPI.Services
{
    public class ProfileService : IProfileService
    {
        private AuthDbContext DbContext;

        public ProfileService(AuthDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();

                var user = DbContext.GetUserById(subjectId);

                context.IssuedClaims = DbContext
                    .GetUserClaimsByUserId(user.UserId)
                    .Select(item => new Claim(item.ClaimType, item.ClaimValue))
                    .ToList();

                return Task.FromResult(0);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = DbContext.GetUserById(context.Subject.GetSubjectId());

            context.IsActive = user != null && user.Active == true;

            return Task.FromResult(0);
        }
    }
}
