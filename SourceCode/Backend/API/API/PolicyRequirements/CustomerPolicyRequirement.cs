using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.PolicyRequirements
{
    public class CustomerPolicyRequirement : AuthorizationHandler<CustomerPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerPolicyRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "role" && claim.Value == "Customer"))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.FromResult(0);
        }
    }
}
