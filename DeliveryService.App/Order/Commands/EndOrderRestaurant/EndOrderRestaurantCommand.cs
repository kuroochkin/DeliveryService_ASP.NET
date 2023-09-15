using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.EndOrderRestaurant;

public record EndOrderRestaurantCommand(
	string ManagerId,
	string OrderId
	) : IRequest<ErrorOr<bool>>;


