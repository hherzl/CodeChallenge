using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthAPI
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("snacksapi", "Snacks API")
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "snacksapi"
                    }
                }
            };

        public static List<TestUser> GetUsers()
            => new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "johnd",
                    Password = "password1"
                }
            };
    }
}
