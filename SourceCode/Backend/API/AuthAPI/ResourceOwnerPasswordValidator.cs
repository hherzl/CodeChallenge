using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthAPI
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IAuthRepository Repository;

        public ResourceOwnerPasswordValidator(IAuthRepository repository)
        {
            Repository = repository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (Repository.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(Repository.GetUserByUserName(context.UserName).UserId, "password", null, "local", null);

                return Task.FromResult(context.Result);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match.", null);

            return Task.FromResult(context.Result);
        }
    }
}
