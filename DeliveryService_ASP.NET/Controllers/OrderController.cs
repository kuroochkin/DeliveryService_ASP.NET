using MapsterMapper;
using DeliveryService.App.Courier.Commands.AddCourier.AddOrder;
using DeliveryService.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryService.API.Controllers
{
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
		public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
		{
			var command = _mapper.Map<CreateOrderCommand>(request);

			var result = await _mediator.Send(command);

			return result.Match(
				coursesResult => Ok(result.Value),
				errors => Problem("Ошибка")
				);
		}
	}
}
