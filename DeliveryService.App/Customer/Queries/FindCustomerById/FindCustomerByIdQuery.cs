using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Queries.FindCustomerById;

public record FindCustomerByIdQuery(
	string CustomerId) : IRequest<ErrorOr<bool>>;

