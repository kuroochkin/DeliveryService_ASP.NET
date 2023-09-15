using DeliveryService.Contracts.Order;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.CreateOrder
{
    public record CreateOrderCommand(
        string CustomerId,
        string Description,
        List<GetProductRequest> Products) : IRequest<ErrorOr<bool>>;
}
