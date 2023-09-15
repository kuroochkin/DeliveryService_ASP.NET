using ErrorOr;
using MediatR;

namespace DeliveryService.App.Manager.Commands.JoinRestaurant;

public record JoinRestaurantCommand(
	string ManagerId,
	string RestaurantId
	) : IRequest<ErrorOr<bool>>;

