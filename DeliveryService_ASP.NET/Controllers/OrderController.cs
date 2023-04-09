﻿using MapsterMapper;
using DeliveryService.App.Courier.Commands.AddCourier.AddOrder;
using DeliveryService.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.App.Order.Commands.CompleteOrder;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.Contracts.Order.Get;
using static DeliveryService.App.Common.Errors.Errors;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer;
using DeliveryService.App.Order.Queries.GetOrdersUser.Courier;

namespace DeliveryService.API.Controllers
{
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

		[HttpGet("detailsOrder/{orderId}")]
		public async Task<IActionResult> GetDetailsOrder(string orderId)
		{
			var query = new GetOrderDetailsQuery(orderId);
			
			var orderResult = await _mediator.Send(query);

			return orderResult.Match(
				order => Ok(_mapper.Map<GetOrderDetailsResponse>(order)),
				errors => Problem("Ошибка")
			);
		}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> GetAllOrdersByCustomerId(string customerId)
		{
			var query = new GetOrdersCustomerQuery(customerId);
			
			var orderResult = await _mediator.Send(query);

			return orderResult.Match(
				orders => Ok(_mapper.Map<GetOrdersCustomerResponse>(orders)),
				errors => Problem("Ошибка")
			);

		}

		[HttpGet("courier/{courierId}")]
		public async Task<IActionResult> GetAllOrdersByCourierId(string courierId)
		{
			var query = new GetOrdersCourierQuery(courierId);

			var orderResult = await _mediator.Send(query);

			return orderResult.Match(
				orders => Ok(_mapper.Map<GetOrdersCourierResponse>(orders)),
				errors => Problem("Ошибка")
			);

		}

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

		[HttpPost("confirm")]
		public async Task<IActionResult> ConfirmOrder(ConfirmOrderRequest request)
		{
			var command = _mapper.Map<ConfirmOrderCommand>(request);

			var result = await _mediator.Send(command);

			return result.Match(
				orderResult => Ok(result.Value),
				errors => Problem("Ошибка")
				);
		}

		[HttpPost("comlete")]
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
}
