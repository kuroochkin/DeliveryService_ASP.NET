using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Manager.Commands.JoinRestaurant;

public class JoinRestaurantCommandHandler
	: IRequestHandler<JoinRestaurantCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public JoinRestaurantCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<bool>> Handle(
		JoinRestaurantCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.ManagerId, out var managerId))
		{
			return Errors.Manager.InvalidId;
		}

		if (!Guid.TryParse(request.RestaurantId, out var restaurantId))
		{
			return Errors.Restaurant.InvalidId;
		}

		var restaurant = await _unitOfWork.Restaurants.FindById(restaurantId);
		if (restaurant is null)
		{
			return Errors.Restaurant.NotFound;
		}

		var manager = await _unitOfWork.Managers.FindById(managerId);
		if (manager is null)
		{
			return Errors.Manager.NotFound;
		}

		// Привязка менеджера к конкретному ресторану
		manager.Restaurant = restaurant;

		return await _unitOfWork.CompleteAsync();
	}
}
