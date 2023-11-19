using DeliveryService.Services.OrderAPI.App.Customer.Vm;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Customer.Queries.GetCustomerDetails;

public record GetCustomerDetailsQuery(
	string CustomerId) : IRequest<ErrorOr<CustomerDetailsVm>>;
