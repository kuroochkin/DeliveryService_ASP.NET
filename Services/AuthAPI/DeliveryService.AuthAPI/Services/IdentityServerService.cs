using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Model.Requests;
using DeliveryService.AuthAPI.Settings;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DeliveryService.AuthAPI.Services
{
    /// <summary>
    /// Сервис, взаимодейтсвующий с IdentityServer, для получения токена и информации по пользователю
    /// </summary>
    public class IdentityServerService
    {
        private readonly IdentityServerSettings _identityServerSettings;
        private string _userInfoAddress;

        public IdentityServerService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            _identityServerSettings = identityServerSettings.Value;
            _userInfoAddress = $"{_identityServerSettings.Url}/connect/userinfo";
        }

        public async Task<TokenModel> GetToken(LoginRequest request)
        {
            var httpClient = new HttpClient();

            // Имитация запроса discovery-информации
            var disco = await httpClient.GetDiscoveryDocumentAsync(_identityServerSettings.Url);

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            //Получаем ответ от IdentityServer-а
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m.client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",

                UserName = request.Username,
                Password = request.Password,
                Scope = "openid profile scope1 roles",
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            var response = await httpClient.GetUserInfoAsync(new UserInfoRequest()
            {
                Address = _userInfoAddress,
                Token = tokenResponse.AccessToken
            });

            if (response.IsError)
                throw new Exception("Invalid accessToken");

            var userInfoResponse = JsonConvert.DeserializeObject<UserInfoResponse>(response.Raw);

            return new TokenModel
            {
                AccessToken = tokenResponse.AccessToken,
                TokenType = tokenResponse.TokenType,

                Username = userInfoResponse.Name,
                Role = userInfoResponse.Role,
            };
        }
    }

    /// <summary>
    /// Модель ответа от метода /connect/userinfo
    /// </summary>
    public class UserInfoResponse
    {
        public string Role { get; set; }
        public string Name { get; set; }
    }
}
