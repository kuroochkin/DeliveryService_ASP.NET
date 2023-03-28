using DeliveryService.App.Auth.Common;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Auth.Commands;

public record RegisterCommand(
	string FirstName,
	string LastName,
	string Email,
	string Password,
	bool IsCustomer) : IRequest<ErrorOr<AuthenticationResult>>;
