using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.PolicyRequirements
{
    public class AdministratorPolicyRequirement : AuthorizationHandler<AdministratorPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorPolicyRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "client_role" && c.Value == "Administrator"))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.FromResult(0);
        }
    }
}
