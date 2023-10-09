using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeliveryService.Services.PaymentAPI.Controllers;

//Authorize!
public class ApiController : ControllerBase
{
	protected string? GetUserId()
	{
		return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}
