namespace DeliveryService.Contracts.Order;

public record CreateOrderRequest(
	string CustomerId,
	string Description);

public record CreateOrderResponse(
	string CustomerId,
	string Description);

