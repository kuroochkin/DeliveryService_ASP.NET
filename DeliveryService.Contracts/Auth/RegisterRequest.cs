namespace DeliveryService.Contracts.Auth;

public record RegisterRequest(
	string LastName,
	string FirstName,
	string Password,
	string Email,
	string Role);

