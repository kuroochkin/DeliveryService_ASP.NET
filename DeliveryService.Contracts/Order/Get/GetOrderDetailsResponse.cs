using DeliveryService.Domain.Product;

namespace DeliveryService.Contracts.Order.Get;

public record GetOrderDetailsResponse(
	string OrderId,
	string Description,
	DateTime Created,
	DateTime ConfirmRestaurant,
	DateTime EndRestaurant,
	DateTime ConfirmCourier,
	DateTime End,
	string Status,
	CourierResponse? Courier,
	CustomerResponse Customer,
	List<ProductsOrderResponse> Products);

public record CourierResponse(
	string CourierId,
	string LastName,
	string FirstName);

public record CustomerResponse(
	string CustomerId,
	string LastName,
	string FirstName);

public record ProductsOrderResponse(
	string ProductId,
	string Count,
	string TotalPrice,
	string Thumbnail,
	string Title);


