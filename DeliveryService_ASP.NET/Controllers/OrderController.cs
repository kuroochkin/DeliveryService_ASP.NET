﻿using MapsterMapper;
using DeliveryService.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeliveryService.App.Order.Commands.CompleteOrder;
using Microsoft.AspNetCore.Authorization;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.Contracts.Order.Get;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GelAllOrdersByCustomer;
using DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetAllOrdersByCourier;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GetOrdersByCustomerByStatus;
using DeliveryService.App.Order.Commands.CreateOrder;
using DeliveryService.App.Order.Commands.ConfirmOrderRestaurant;
using DeliveryService.Contracts.Manager;
using DeliveryService.App.Order.Commands.EndOrderRestaurant;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.Contracts.Courier;
using DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersByCourierByStatus;
using DeliveryService.App.Order.Commands.CheckoutPayment;

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
	
	/// <summary>
	///	Получение информация о заказе по его Id
	/// </summary>
	/// <param name="orderId"></param>
	/// <returns></returns>
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

	/// <summary>
	/// Получение всех заказов конкретного заказчика
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Получение всех заказов конкретного заказчика по статусу заказа
	/// </summary>
	/// <param name="orderStatus"></param>
	/// <returns></returns>
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

	/// <summary>
	/// Получение всех заказов конкретного курьера по статусу заказа
	/// </summary>
	/// <param name="orderStatus"></param>
	/// <returns></returns>
	[HttpGet("courierOrders/{orderStatus}")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> GetOrdersByCourierIdByOrderStatus(string orderStatus)
	{
		var courierId = GetUserId();

		var query = new GetOrdersCourierStatusQuery(courierId, orderStatus);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			orders => Ok(_mapper.Map<GetOrdersCourierResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	/// <summary>
	/// Получение всех заказов конкретного курьера
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Запрос по созданию заказа
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
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

	[HttpPost("checkout")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> CheckoutPayment(CheckoutPaymentRequest request)
	{
		var customer = GetUserId();

		var command = _mapper.Map<CheckoutPaymentCommand>((request, customer));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	/// <summary>
	/// Подтверждение заказа менеджером ресторана
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("manager/confirmRestaurant")]
	[Authorize(Roles = "Manager")]
	public async Task<IActionResult> ConfirmOrderByRestaurant(ConfirmOrderRestaurantRequest request)
	{
		var manager = GetUserId();

		var command = _mapper.Map<ConfirmOrderRestaurantCommand>((request, manager));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	/// <summary>
	/// Окончание сборки заказа менеджером ресторана
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("manager/endRestaurant")]
	[Authorize(Roles = "Manager")]
	public async Task<IActionResult> EndOrderByRestaurant(EndOrderRestaurantRequest request)
	{
		var manager = GetUserId();

		var command = _mapper.Map<EndOrderRestaurantCommand>((request, manager));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	/// <summary>
	/// Подтверждение заказа курьером
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("confirm")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> ConfirmOrderByCourier(ConfirmOrderCourierRequest request)
	{
		var courier = GetUserId();

		var command = _mapper.Map<ConfirmOrderCourierCommand>((request, courier));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}

	/// <summary>
	/// Окончание выполнения заказа курьером
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("complete")]
	[Authorize(Roles = "Courier")]
	public async Task<IActionResult> CompleteOrder(EndOrderCourierRequest request)
	{
		var courierId = GetUserId();

		var command = _mapper.Map<EndOrderCourierCommand>((request, courierId));

		var result = await _mediator.Send(command);

		return result.Match(
			orderResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}
