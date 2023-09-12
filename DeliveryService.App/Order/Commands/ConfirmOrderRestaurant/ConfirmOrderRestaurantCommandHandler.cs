using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Commands.ConfirmOrderRestaurant;

public class ConfirmOrderRestaurantCommandHandler
	: IRequestHandler<ConfirmOrderRestaurantCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public ConfirmOrderRestaurantCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<bool>> Handle(
		ConfirmOrderRestaurantCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.RestaurantId, out var restaurantId))
		{
			return Errors.Restaurant.InvalidId;
		}

		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomer(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}

		if (order.GetStatus >= OrderStatus.ConfirmedRestaurant)
			return false;

		order.Status = OrderStatus.ConfirmedRestaurant;

		return await _unitOfWork.CompleteAsync();
	}
}
