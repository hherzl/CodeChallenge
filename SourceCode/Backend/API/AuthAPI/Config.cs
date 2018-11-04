using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace AuthAPI
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("SnacksApi", "Snacks API")
                {
                    UserClaims =
                    {
                        "Customer",
                        "Administrator"
                    }
                }
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "snackscustomer",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

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
                        new Claim("role", "Customer")
                    }
                },
                new Client
                {
                    ClientId = "snacksadministrator",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

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
                        new Claim("role", "Administrator")
                    }
                }
            };
    }
}
