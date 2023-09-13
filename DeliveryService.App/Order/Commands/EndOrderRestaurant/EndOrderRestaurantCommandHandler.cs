using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Commands.EndOrderRestaurant;

public class EndOrderRestaurantCommandHandler
	: IRequestHandler<EndOrderRestaurantCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public EndOrderRestaurantCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<bool>> Handle(
		EndOrderRestaurantCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.ManagerId, out var managerId))
		{
			return Errors.Manager.InvalidId;
		}

		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndManager(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}

		var manager = await _unitOfWork.Managers.FindManagerWithRestaurantById(managerId);
		if (manager is null)
		{
			return Errors.Manager.NotFound;
		}

		if (order.GetStatus >= OrderStatus.EndRestaurant || order.GetStatus < OrderStatus.ConfirmedRestaurant)
			return false;

		order.Status = OrderStatus.EndRestaurant;

		order.EndRestaurant = DateTime.Now;

		return await _unitOfWork.CompleteAsync();
	}
}

