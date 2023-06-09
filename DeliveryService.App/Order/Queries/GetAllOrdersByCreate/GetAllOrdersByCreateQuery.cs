using DeliveryService.App.Order.Queries.GetOrdersUser;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetAllOrdersByCreate;

public record GetAllOrdersByCreateQuery() : IRequest<ErrorOr<OrdersUserVm>>;
