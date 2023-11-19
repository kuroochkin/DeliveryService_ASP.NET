using AutoMapper;
using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Model.Requests;
using DeliveryService.AuthAPI.Model.Responses;
using DeliveryService.AuthAPI.Services;
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
        private readonly AuthService _authService;
        private IMapper _mapper;

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

        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterRequest request)
        {
            var resService = await _authService.RegisterCustomer(request);
            var response = _mapper.Map<AuthResponse>(resService);
            return Ok(response);
        }
    }
}
