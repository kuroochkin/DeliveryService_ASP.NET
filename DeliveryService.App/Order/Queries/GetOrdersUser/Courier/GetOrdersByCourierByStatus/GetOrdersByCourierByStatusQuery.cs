using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersByCourierByStatus;

public record GetOrdersCourierStatusQuery(
	string CourierId,
	string OrderStatus) : IRequest<ErrorOr<OrdersUserVm>>;
