namespace DeliveryService.Services.OrderAPI.App.Customer.Vm;

public record CustomerDetailsVm(
	string Id,
	string Email,
	string Password,
	string LastName,
	string FirstName,
	DateTime BirthDay,
	int CountOrder
	);