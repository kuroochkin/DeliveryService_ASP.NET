namespace DeliveryService.App.Order.Queries.GetOrderDetails;

public record OrderDetailsVm(
	string OrderId,
	string Description,
	DateTime Created,
	CourierVm Courier,
	CustomerVm Customer
	);

public record CourierVm(
	string CourierId,
	string LastName,
	string FirstName,
	string Patronymic);

public record CustomerVm(
	string CustomerId,
	string LastName,
	string FirstName,
	string Patronymic);
