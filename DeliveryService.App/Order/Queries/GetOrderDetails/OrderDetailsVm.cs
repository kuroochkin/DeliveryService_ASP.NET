using DeliveryService.Domain.Product;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Queries.GetOrderDetails;

public record OrderDetailsVm(
	string OrderId,
	string Description,
	DateTime Created,
	DateTime ConfirmRestaurant,
	DateTime EndRestaurant,
	DateTime ConfirmCourier,
	DateTime End,
	OrderStatus Status,
	CourierVm? Courier,
	CustomerVm Customer,
	List<ProductOrderVm> Products
	);

public record CourierVm(
	string ?CourierId,
	string ?LastName,
	string ?FirstName);

public record CustomerVm(
	string CustomerId,
	string LastName,
	string FirstName);

public record ProductOrderVm(
	string ProductId,
	string Count,
	string TotalPrice,
	string Thumbnail,
	string Title
	);
