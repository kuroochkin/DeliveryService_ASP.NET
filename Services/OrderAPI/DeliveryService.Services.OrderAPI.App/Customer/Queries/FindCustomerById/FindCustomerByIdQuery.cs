using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Customer.Queries.FindCustomerById;

public record FindCustomerByIdQuery(
	string CustomerId) : IRequest<ErrorOr<bool>>;
