using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;

public record GetOrdersCourierByStatusQuery(
	string CourierId,
	string OrderStatus) : IRequest<ErrorOr<OrdersUserVm>>;
