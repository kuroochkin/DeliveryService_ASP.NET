using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Commands.CompleteOrder;

public class CompleteOrderCommandHandler
	: IRequestHandler<CompleteOrderCommand, ErrorOr<bool>>
{
	
	private readonly IUnitOfWork _unitOfWork;

	public CompleteOrderCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle
		(CompleteOrderCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourier(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}


		if (order.GetStatus != OrderStatus.ConfirmedCourier)
			return false;

		//Меняем статус заказа
		order.Status = OrderStatus.Complete;

		//Устанавливаем время завершения заказа
		order.End = DateTime.Now;

		return await _unitOfWork.CompleteAsync();

	}
}
