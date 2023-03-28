using DeliveryService.App.Auth.Common;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Auth.Queries.Login;

public record LoginQuery(
	string Email,
	string Password
	) : IRequest<ErrorOr<AuthenticationResult>>;