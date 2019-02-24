using System.Linq;
using System.Security.Claims;
using IdentityModel;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Identities.First().Claims.First(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
    }
}
