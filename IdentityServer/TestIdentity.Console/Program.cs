using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestIdentity.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание HttpClient для обращения к Identity Server

            System.Console.ReadKey();

            var token = GetToken().Result;

            // Печать полученного access токена
            //Console.WriteLine(tokenResponse.AccessToken);
        }

        private async static Task<string> GetToken()
        {
            var httpClient = new HttpClient();

            // Имитация запроса discovery-информации
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5007");

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m.client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",

                UserName = "alice",
                Password = "Pass123$",
                Scope = "scope1"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
    }
}
