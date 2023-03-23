using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Commands.CreateCustomer;

public record СreateCustomerCommand(
	string LastName,
	string FirstName,
	string Patromymic) : IRequest<ErrorOr<bool>>;
