using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

namespace AuthAPI
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("SnacksApi", "Snacks API")
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "snacksclient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "SnacksApi"
                    },
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Role, "Customer"),
                        new Claim(JwtClaimTypes.Role, "Administrator")
                    }
                }
            };
    }
}
