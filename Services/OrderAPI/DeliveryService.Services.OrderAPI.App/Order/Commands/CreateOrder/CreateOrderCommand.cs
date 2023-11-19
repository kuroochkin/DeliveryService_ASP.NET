using DeliveryService.Services.OrderAPI.Contracts.Order;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Order.Commands.CreateOrder;

    public record CreateOrderCommand(
		string CustomerId,
		string Description,
		string Card,
		decimal TotalPrice,
		List<GetProductRequest> Products) : IRequest<ErrorOr<bool>>;

