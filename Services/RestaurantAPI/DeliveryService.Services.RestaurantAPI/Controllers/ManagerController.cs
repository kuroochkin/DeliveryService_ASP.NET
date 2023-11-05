using DeliveryService.Services.RestaurantAPI.App.Manager.Commands.JoinRestaurant;
using DeliveryService.Services.RestaurantAPI.Contracts.Manager;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.RestaurantAPI.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : Controller
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public ManagerController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	/// <summary>
	/// Привязка менеджера к ресторану
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	//[HttpPost("addRestaurant")]
	//[Authorize(Roles = "Manager")]
	//public async Task<IActionResult> AddRestaurant(JoinRestaurantRequest request)
	//{
	//	var managerId = GetUserId();

	//	var command = _mapper.Map<JoinRestaurantCommand>((request, managerId));

	//	var result = await _mediator.Send(command);

	//	return result.Match(
	//		Result => Ok(result.Value),
	//		errors => Problem("Ошибка")
	//		);
	//}
}


