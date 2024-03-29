﻿using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Commands.ConfirmOrder;

public class ConfirmOrderCourierCommandHandler
	: IRequestHandler<ConfirmOrderCourierCommand, ErrorOr<bool>>
{
	
	private readonly IUnitOfWork _unitOfWork;

	public ConfirmOrderCourierCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle
		(ConfirmOrderCourierCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Courier.InvalidId;
		}

		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}


		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.NotFound;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndManager(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}

		if (order.GetStatus >= OrderStatus.ConfirmedCourier || order.GetStatus < OrderStatus.EndRestaurant)
			return false;

		//Добавляем курьера в заказ
		order.Courier = courier;

		//Меняем статус заказа
		order.Status = OrderStatus.ConfirmedCourier;

		order.ConfirmedCourier = DateTime.Now;

		//Добавляем заказ в копилку заказов курьера
		courier.AddOrder(order);

		return await _unitOfWork.CompleteAsync();
	}
}
