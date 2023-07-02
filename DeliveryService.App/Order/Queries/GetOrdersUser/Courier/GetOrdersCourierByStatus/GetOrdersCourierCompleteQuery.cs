using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;

public record GetOrdersCourierCompleteQuery(
	string CourierId) : IRequest<ErrorOr<OrdersUserVm>>;
