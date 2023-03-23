namespace DeliveryService.Contracts.Order;

public record CreateOrderRequest(
	string CourierId,
	string CustomerId,
	string Description);

public record CreateOrderResponse(
	string CourierId,
	string CustomerId,
	string Description);

