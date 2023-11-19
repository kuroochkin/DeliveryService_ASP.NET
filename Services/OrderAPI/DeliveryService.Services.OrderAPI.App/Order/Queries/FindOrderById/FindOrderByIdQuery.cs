using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Order.Queries.FindOrderById;

public record FindOrderByIdQuery(
	string OrderId) : IRequest<ErrorOr<bool>>;
