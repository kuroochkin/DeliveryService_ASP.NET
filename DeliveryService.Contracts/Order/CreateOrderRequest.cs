namespace DeliveryService.Contracts.Order;

public record CreateOrderRequest(
	string Description);

public record CreateOrderResponse(
	string Description);

