using ErrorOr;
using MediatR;

namespace DeliveryService.Services.RestaurantAPI.App.Manager.Commands.JoinRestaurant;

public record JoinRestaurantCommand(
	string ManagerId,
	string RestaurantId
	) : IRequest<ErrorOr<bool>>;