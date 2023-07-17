namespace DeliveryService.App.Customer.Queries;

public record CustomerDetailsVm(
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
