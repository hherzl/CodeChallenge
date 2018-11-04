using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClientName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Identities.First().Claims.First(item => item.Type == "client_id").Value;
    }
}
