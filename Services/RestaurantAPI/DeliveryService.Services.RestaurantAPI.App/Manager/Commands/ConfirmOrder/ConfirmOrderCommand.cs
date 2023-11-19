using ErrorOr;
using MediatR;

namespace DeliveryService.Services.RestaurantAPI.App.Manager.Commands.ConfirmOrder;

public record ConfirmOrderRestaurantCommand(
		string ManagerId,
		string OrderId) : IRequest<ErrorOr<bool>>;
