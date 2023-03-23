using ErrorOr;
using MediatR;

namespace DeliveryService.App.Courier.Commands.СreateCourier;

public record СreateCourierCommand(
	string LastName,
	string FirstName,
	string Patromymic) : IRequest<ErrorOr<bool>>;
