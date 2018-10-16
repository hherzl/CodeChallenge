using System.Collections.Generic;
using IdentityServer4;
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
                    ClientId = "client",

                    // No interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // Secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret123".Sha256())
                    },

                    // Scopes that client has access to
                    AllowedScopes =
                    {
                        "SnacksApi"
                    }
                },
                new Client
                {
                    ClientId = "jsclient",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:5700"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5700"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:5003"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "SnacksApi"
                    }
                }
            };
    }
}
