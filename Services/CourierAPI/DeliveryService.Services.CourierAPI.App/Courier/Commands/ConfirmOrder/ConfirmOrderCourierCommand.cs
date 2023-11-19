using ErrorOr;
using MediatR;

namespace DeliveryService.Services.CourierAPI.App.Courier.Commands.ConfirmOrder;

public record class ConfirmOrderCourierCommand(
		string CourierId,
		string OrderId) : IRequest<ErrorOr<bool>>;
