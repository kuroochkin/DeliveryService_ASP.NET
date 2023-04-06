using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeliveryService.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
	protected string? GetUserId()
	{
		return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}
