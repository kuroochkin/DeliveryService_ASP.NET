using ErrorOr;
using MediatR;

namespace DeliveryService.Services.RestaurantAPI.App.Manager.Commands.CompleteOrder;

public record CompleteOrderRestaurantCommand(
		string ManagerId,
		string OrderId) : IRequest<ErrorOr<bool>>;
