using MapsterMapper;
using DeliveryService.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.App.Order.Commands.CompleteOrder;
using Microsoft.AspNetCore.Authorization;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.Contracts.Order.Get;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GelAllOrdersByCustomer;
using DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetAllOrdersByCourier;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GetOrdersByCustomerByStatus;
using DeliveryService.App.Order.Queries.GetAllOrdersByCreate;
using DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;
using DeliveryService.App.Order.Commands.CreateOrder;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public OrderController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}
	
	//ГОТОВО!!!
	[HttpGet("{orderId}")]
	public async Task<IActionResult> GetDetailsOrder(string orderId)
	{
		var query = new GetOrderDetailsQuery(orderId);
		
		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			order => Ok(_mapper.Map<GetOrderDetailsResponse>(order)),
			errors => Problem("Ошибка")
		);
	}

	//ГОТОВО!!!
	[HttpGet("customerOrders")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> GetAllOrdersByCustomerId()
	{
		var customerId = GetUserId();

		var query = new GetOrdersCustomerQuery(customerId);
		
		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCustomerResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	[HttpGet("allOrdersByCreate")]
	public async Task<IActionResult> GetAllOrdersByCreate()
	{

		var query = new GetAllOrdersByCreateQuery();

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetAllOrdersByCreateResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	//ГОТОВО!!!
	[HttpGet("customerOrders/{orderStatus}")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> GetOrdersByCustomerIdByOrderStatus(string orderStatus)
	{
		var customerId = GetUserId();

		var query = new GetOrdersCustomerStatusQuery(customerId, orderStatus);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCustomerResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	[HttpGet("courierOrders/Progress")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> GetOrdersCourierByOrderProgress()
	{
		var courierId = GetUserId();

		var query = new GetOrdersCourierProgressQuery(courierId);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCourierResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	[HttpGet("courierOrders/Complete")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> GetOrdersCourierByOrderComplete()
	{
		var courierId = GetUserId();

		var query = new GetOrdersCourierCompleteQuery(courierId);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCourierResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	//ГОТОВО!!!
	[HttpGet("courierOrders")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> GetAllOrdersByCourierId()
	{
		var courierId = GetUserId();

		var query = new GetOrdersCourierQuery(courierId);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCourierResponse>(orders)),
			errors => Problem("Ошибка")
		);

	}

	//ГОТОВО!!!
	[HttpPost("create")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
	{
		var customer = GetUserId();

		var command = _mapper.Map<CreateOrderCommand>((request, customer));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	[HttpPost("confirmRestaurant")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> ConfirmOrder(ConfirmOrderRequest request)
	{
		var courier = GetUserId();

		var command = _mapper.Map<ConfirmOrderCommand>((request, courier));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}


	//[HttpPost("confirm")]
	//[Authorize(Roles = "Courier")]
	//public async Task<IActionResult> ConfirmOrder(ConfirmOrderRequest request)
	//{
	//	var courier = GetUserId();

	//	var command = _mapper.Map<ConfirmOrderCommand>((request,courier));

	//	var result = await _mediator.Send(command);

	//	return result.Match(
	//		orderResult => Ok(result.Value),
	//		errors => Problem("Ошибка")
	//		);
	//}

	[HttpPost("complete")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> CompleteOrder(CompleteOrderRequest request)
	{
		var command = _mapper.Map<CompleteOrderCommand>(request);

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}
