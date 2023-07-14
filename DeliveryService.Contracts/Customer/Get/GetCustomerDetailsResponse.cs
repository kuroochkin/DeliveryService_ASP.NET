namespace DeliveryService.Contracts.Customer.Get;

public record GetCustomerDetailsResponse(
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

