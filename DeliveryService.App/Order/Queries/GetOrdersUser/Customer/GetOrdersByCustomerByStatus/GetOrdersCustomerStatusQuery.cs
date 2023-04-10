using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GetOrdersByCustomerByStatus;

public record GetOrdersCustomerStatusQuery(
	string CustomerId,
	string OrderStatus) : IRequest<ErrorOr<OrdersUserVm>>;


