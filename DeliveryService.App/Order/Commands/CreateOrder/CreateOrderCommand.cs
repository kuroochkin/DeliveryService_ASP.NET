using ErrorOr;
using MediatR;

namespace DeliveryService.App.Courier.Commands.AddCourier.AddOrder
{
	public record CreateOrderCommand(
		string CourierId,
		string CustomerId,
		string Description) : IRequest<ErrorOr<bool>>;
}
