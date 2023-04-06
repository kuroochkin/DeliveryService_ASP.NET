using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Customer;

public record GetOrdersCustomerQuery(
	string CustomerId) : IRequest<ErrorOr<OrdersUserVm>>;