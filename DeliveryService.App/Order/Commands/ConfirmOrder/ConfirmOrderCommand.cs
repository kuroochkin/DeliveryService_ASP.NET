using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.ConfirmOrder;

public record ConfirmOrderCommand(
		string CourierId,
		string OrderId) : IRequest<ErrorOr<bool>>;
