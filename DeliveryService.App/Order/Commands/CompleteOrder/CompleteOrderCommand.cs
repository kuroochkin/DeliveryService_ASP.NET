using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.CompleteOrder;

public record CompleteOrderCommand(
		string OrderId) : IRequest<ErrorOr<bool>>;