using DeliveryService.AuthAPI.Model;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DeliveryService.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResponse = await GetToken(request);
            return Ok(authResponse);
        }

        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.Username };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Создание роли, если она не существует
                if (!await _roleManager.RoleExistsAsync("client"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("client"));
                }

                // Добавление пользователя в роль
                await _userManager.AddToRoleAsync(user, "client");
            }

            return Ok(true);
        }

        private async Task<AuthResponse> GetToken(LoginRequest request)
        {
            var authResponse = new AuthResponse();

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

                UserName = request.Username, //"alice",
                Password = request.Password, //"Pass123$",
                Scope = "openid profile scope1 roles",
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            var t1 = JsonConvert.SerializeObject(tokenResponse);
            var returnObj = JsonConvert.DeserializeObject<Token>(t1);

            authResponse.AccessToken = returnObj.AccessToken;
            authResponse.TokenType = returnObj.TokenType;
            authResponse.ExpiresIn = returnObj.ExpiresIn;

            var client = new HttpClient();
            var userInfoRequest = new UserInfoRequest()
            {
                Address = "http://localhost:5007/connect/userinfo",
                Token = returnObj.AccessToken
            };

            var response = client.GetUserInfoAsync(userInfoRequest).Result;
            if (response.IsError)
                throw new Exception("Invalid accessToken");

            var responseObject = JsonConvert.DeserializeObject<Model.UserInfoResponse>(response.Raw);

            authResponse.Username = responseObject.Name;
            authResponse.Role = responseObject.Role;


            return authResponse;
        }
    }
}
