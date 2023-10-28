using DeliveryService.Services.OrderAPI.App.Order.Commands.CreateOrder;
using DeliveryService.Services.OrderAPI.Contracts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.OrderAPI.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public OrderController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPost("create")]
	//[Authorize(Roles = "Customer")]
	public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
	{
		var command = _mapper.Map<CreateOrderCommand>(request);

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}
