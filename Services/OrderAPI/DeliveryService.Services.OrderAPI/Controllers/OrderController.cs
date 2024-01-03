using DeliveryService.Services.OrderAPI.App.Order.Queries.FindOrderById;
using MapsterMapper;
using MediatR;
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

	[HttpGet("find/{orderId}")]
	public async Task<IActionResult> FindOrderById(string orderId)
	{
		var query = new FindOrderByIdQuery(orderId);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(orderResult.Value),
			errors => Problem("Ошибка")
		);
	}
}
