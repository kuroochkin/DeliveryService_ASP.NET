using DeliveryService.Services.RestaurantAPI.App.Manager.Commands.CompleteOrder;
using DeliveryService.Services.RestaurantAPI.App.Manager.Commands.ConfirmOrder;
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
	/// <param name = "request" ></ param >
	/// < returns ></ returns >
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

	/// <summary>
	/// Подтверждение заказа менеджером ресторана
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("confirmRestaurant")]
	//[Authorize(Roles = "Manager")]
	public async Task<IActionResult> ConfirmOrderByRestaurant(ConfirmOrderRestaurantRequest request)
	{
		var command = _mapper.Map<ConfirmOrderRestaurantCommand>(request);

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	[HttpPost("completeRestaurant")]
	//[Authorize(Roles = "Manager")]
	public async Task<IActionResult> CompleteOrderByRestaurant(CompleteOrderRestaurantRequest request)
	{
		var command = _mapper.Map<CompleteOrderRestaurantCommand>(request);

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}


