using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;

public record GetOrdersCourierProgressQuery(
	string CourierId) : IRequest<ErrorOr<OrdersUserVm>>;
