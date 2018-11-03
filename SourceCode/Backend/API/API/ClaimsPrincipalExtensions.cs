using System.Linq;
using System.Security.Claims;

namespace API
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClientName(this ClaimsPrincipal claimsPrincipal)
            => (claimsPrincipal.Identities.First().Claims.ToList()[5] as Claim).Value;
    }
}
