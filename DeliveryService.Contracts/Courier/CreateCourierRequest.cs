namespace DeliveryService.Contracts.Courier;

public record CreateCourierRequest(
	string LastName,
	string FirstName,
	string Patronymic);

public record CreateCourierResponse(
	string LastName,
	string FirstName,
	string Patronymic);
