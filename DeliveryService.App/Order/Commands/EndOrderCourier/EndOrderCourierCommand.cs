using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.CompleteOrder;

public record EndOrderCourierCommand(
		string CourierId,
		string OrderId) : IRequest<ErrorOr<bool>>;