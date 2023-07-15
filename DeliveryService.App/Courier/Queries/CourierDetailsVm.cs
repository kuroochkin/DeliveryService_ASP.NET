namespace DeliveryService.App.Courier.Queries;

public record CourierDetailsVm(
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
