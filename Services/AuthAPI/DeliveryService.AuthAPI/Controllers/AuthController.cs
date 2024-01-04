using AutoMapper;
using DeliveryService.AuthAPI.Data;
using DeliveryService.AuthAPI.Model.Requests;
using DeliveryService.AuthAPI.Model.Responses;
using DeliveryService.AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryService.AuthAPI.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(AuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var resService = await _authService.Login(request);
        var response = _mapper.Map<AuthResponse>(resService);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var resService = await _authService.RegisterCustomer(request);
        var response = _mapper.Map<AuthResponse>(resService);
        return Ok(response);
    }

    [HttpGet("get-by-id/{Id}")]
    public async Task<IActionResult> GetUserDetailsById(string Id)
    {
        var user = await _authService.GetUserById(Id);
        return Ok(user);
    }
}
