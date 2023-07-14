using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Queries.GetCustomerDetails;

public record GetCustomerDetailsQuery(
	string CustomerId) : IRequest<ErrorOr<CustomerDetailsVm>>;

