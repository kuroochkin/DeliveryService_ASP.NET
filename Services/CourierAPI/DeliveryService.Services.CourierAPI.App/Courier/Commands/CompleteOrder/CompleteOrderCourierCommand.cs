using ErrorOr;
using MediatR;

namespace DeliveryService.Services.CourierAPI.App.Courier.Commands.CompleteOrder;

public record CompleteOrderCourierCommand(
		string CourierId,
		string OrderId) : IRequest<ErrorOr<bool>>;

