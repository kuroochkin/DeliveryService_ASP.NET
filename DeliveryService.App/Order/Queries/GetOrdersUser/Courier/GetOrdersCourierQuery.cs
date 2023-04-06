using DeliveryService.App.Order.Queries.GetOrdersUser.Customer;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier;

public record GetOrdersCourierQuery(
	string CourierId) : IRequest<ErrorOr<OrdersUserVm>>;