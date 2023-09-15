using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.ConfirmOrder;

public record ConfirmOrderCourierCommand(
		string CourierId,
		string OrderId) : IRequest<ErrorOr<bool>>;
