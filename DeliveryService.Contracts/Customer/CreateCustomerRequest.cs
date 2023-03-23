
namespace DeliveryService.Contracts.Customer;

public record CreateCustomerRequest(
	string LastName,
	string FirstName,
	string Patronymic);

public record CreateCustomerResponse(
	string LastName,
	string FirstName,
	string Patronymic);
