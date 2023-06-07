using DeliveryService.Contracts.Order;
using DeliveryService.Domain.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Courier.Commands.AddCourier.AddOrder
{
	public record CreateOrderCommand(
		string CustomerId,
		string Description,
		List<GetProductRequest> Products) : IRequest<ErrorOr<bool>>;
}
