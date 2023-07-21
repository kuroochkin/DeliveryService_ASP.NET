using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Commands.EditProfile;

public record EditProfileCommand(
	string CustomerId,
	DateTime Birthday,
	string City,
	string CountOrder,
	string Email,
	string Password,
	string FirstName,
	string LastName,
	string PhoneNumber) : IRequest<ErrorOr<bool>>;
