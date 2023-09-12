using DeliveryService.App.Auth.Common;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role) : IRequest<ErrorOr<AuthenticationResult>>;
