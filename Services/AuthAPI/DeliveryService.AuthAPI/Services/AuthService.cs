using DeliveryService.AuthAPI.Constants;
using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Model.Requests;
using DeliveryService.AuthAPI.Model.Responses;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace DeliveryService.AuthAPI.Services
{
    /// <summary>
    /// Сервис, для регистрации пользователей в БД, которую использует IdentityServer и логина
    /// </summary>
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityServerService _identityServerService;
        private readonly JwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IdentityServerService identityServerService,
            JwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityServerService = identityServerService;
            _jwtService = jwtService;
        }

        public async Task<ResultService> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                return new ResultService
                {
                    Success = false,
                    Error = "wrong username"
                };
            }

            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!checkPassword.Succeeded)
            {
                return new ResultService
                {
                    Success = false,
                    Error = "wrong password"
                };
            }

            //var token = await _identityServerService
            //    .GetToken(new LoginRequest { Password = request.Password, Username = request.Username });

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.CreateToken(user, roles.FirstOrDefault());

            return new ResultService
            {
                Success = true,
                Token = token
            };
        }

        public async Task<ResultService> RegisterCustomer(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is not null)
            {
                return new ResultService
                {
                    Success = false,
                    Error = "username already exists"
                };
            }

            var newUser = new ApplicationUser { UserName = request.Username };
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                // Создание роли, если она не существует
                if (!await _roleManager.RoleExistsAsync(RoleConstants.CUSTOMER))
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleConstants.CUSTOMER));
                }

                // Добавление пользователя в роль
                await _userManager.AddToRoleAsync(newUser, RoleConstants.CUSTOMER);
            }
            else
            {
                return new ResultService
                {
                    Success = false,
                };
            }

            //var token = await _identityServerService
            //    .GetToken(new LoginRequest { Password = request.Password, Username = request.Username });

            var roles = await _userManager.GetRolesAsync(newUser);

            var token = _jwtService.CreateToken(newUser, roles.FirstOrDefault());

            return new ResultService
            {
                Success = true,
                Token = token
            };
        }
    }

    public class ResultService
    {
        public bool Success { get; set; }
        public AuthTokenResponse Token { get; set; }
        public string Error { get; set; }
    }
}
