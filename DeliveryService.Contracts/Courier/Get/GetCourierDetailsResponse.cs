namespace DeliveryService.Contracts.Courier.Get;

public record GetCourierDetailsResponse(
	string Id,
	string Email,
	string Password,
	string LastName,
	string FirstName,
	DateTime BirthDay,
	int CountOrder,
	string PhoneNumber,
	string City
	);

