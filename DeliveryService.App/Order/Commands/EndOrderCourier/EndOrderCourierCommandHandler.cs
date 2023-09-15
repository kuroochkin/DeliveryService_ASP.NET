using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Commands.CompleteOrder;

public class EndOrderCourierCommandHandler
	: IRequestHandler<EndOrderCourierCommand, ErrorOr<bool>>
{
	
	private readonly IUnitOfWork _unitOfWork;

	public EndOrderCourierCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle
		(EndOrderCourierCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}


		if (order.GetStatus < OrderStatus.ConfirmedCourier || order.GetStatus == OrderStatus.Complete)
			return false;

		//Меняем статус заказа
		order.Status = OrderStatus.Complete;

		//Устанавливаем время завершения заказа
		order.End = DateTime.Now;

		return await _unitOfWork.CompleteAsync();
	}
}
