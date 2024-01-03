using DeliveryService.AuthAPI.Constants;
using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Model.Requests;
using DeliveryService.AuthAPI.Model.Responses;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Reflection;
using DeliveryService.AuthAPI.RabbitMQ.Senders.Interfaces;
using DeliveryService.AuthAPI.RabbitMQ.Messages;

namespace DeliveryService.AuthAPI.Services;

/// <summary>
/// Сервис, для регистрации пользователей в БД, которую использует IdentityServer и логина
/// </summary>
public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IdentityServerService _identityServerService;
    private readonly IRabbitMQCreateUserWithRoleSender _rabbitMQCreateUser;
    private readonly JwtService _jwtService;

    public AuthService(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IdentityServerService identityServerService,
        JwtService jwtService,
        IRabbitMQCreateUserWithRoleSender rabbitMQCreateUser)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _rabbitMQCreateUser = rabbitMQCreateUser;
    }

    public async Task<ResultService> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return new ResultService
            {
                Success = false,
                Error = "Неверный логин"
            };
        }

        var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!checkPassword.Succeeded)
        {
            return new ResultService
            {
                Success = false,
                Error = "Неверный пароль"
            };
        }

        var token = await GetToken(user);

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
                Error = "Такой логин уже занят"
            };
        }

        var newUser = new ApplicationUser { UserName = request.Username };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        var staticRoles = typeof(RoleConstants).GetFields(BindingFlags.Static | BindingFlags.Public);
        var roleValues = staticRoles.Select(field => field.GetValue(null)?.ToString()).ToArray();

        if (!roleValues.Contains(request.Role))
        {
            return new ResultService
            {
                Success = false,
                Error = "Такой роли не существует"
            };
        }

        if (result.Succeeded)
        {
            // Создание роли, если она не существует
            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(request.Role));
            }

            // Добавление пользователя в роль
            await _userManager.AddToRoleAsync(newUser, request.Role);

            //RabbitMQ
            SendMessageAboutCreateUser(newUser, request);
        }
        else
        {
            return new ResultService
            {
                Success = false,
            };
        }

        return new ResultService
        {
            Success = true,
            Token = await GetToken(newUser)
        };
    }

    public async Task<UserDetailsVm> GetUserById(string Id)
    {
        var user = await _userManager.FindByIdAsync(Id);

        if (user == null) 
            throw new ArgumentNullException(nameof(user), $"Пользователь с Id {Id} не найден");

        var userVm = new UserDetailsVm
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };

        return userVm;
    }

    private async Task<AuthTokenResponse> GetToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.CreateToken(user, roles.FirstOrDefault());

        return token;
    }

    private void SendMessageAboutCreateUser(ApplicationUser user, RegisterRequest request)
    {
        var message = new CreateUserWithRoleDTO 
        { 
            UserId = user.Id, 
            UserName = user.UserName,
            LastName = request.LastName,
            FirstName = request.FirstName,
            RoleName = request.Role
        };

        switch (request.Role)
        {
            case "customer":
                _rabbitMQCreateUser.SendMessage(message, "OrderAPI: CreateCustomerQueue");
                break;
            case "courier":
                _rabbitMQCreateUser.SendMessage(message, "CourierAPI: CreateCourierQueue");
                break;
            case "manager":
                _rabbitMQCreateUser.SendMessage(message, "RestaurantAPI: CreateManagerQueue");
                break;
        }
    }
}

public class ResultService
{
    public bool Success { get; set; }
    public AuthTokenResponse Token { get; set; }
    public string Error { get; set; }
}
