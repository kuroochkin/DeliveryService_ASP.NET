namespace DeliveryService.Contracts.Customer;

public record EditCustomerProfileRequest(
	string CustomerId,
	DateTime Birthday,
	string City,
	string CountOrder,
	string Email,
	string Password,
	string FirstName,
	string LastName,
	string PhoneNumber);

