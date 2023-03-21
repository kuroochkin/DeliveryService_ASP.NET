using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(
	string OrderId) : IRequest<ErrorOr<OrderDetailsVm>>;
