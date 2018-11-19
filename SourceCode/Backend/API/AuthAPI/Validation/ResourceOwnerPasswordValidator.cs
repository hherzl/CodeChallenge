using System.Threading.Tasks;
using AuthAPI.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthAPI.Validation
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private AuthDbContext DbContext;

        public ResourceOwnerPasswordValidator(AuthDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (DbContext.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(DbContext.GetUserByUserName(context.UserName).UserId, "password", null, "local", null);

                return Task.FromResult(context.Result);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match.", null);

            return Task.FromResult(context.Result);
        }
    }
}
