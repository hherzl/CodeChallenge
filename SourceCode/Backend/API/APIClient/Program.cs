using System;
using System.Net.Http;
using IdentityModel.Client;

namespace APIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5600").GetAwaiter().GetResult();
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // Request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret123");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").GetAwaiter().GetResult();

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // Call API
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("http://localhost:5700/api/v1/Warehouse").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }

            Console.ReadLine();
        }
    }
}
