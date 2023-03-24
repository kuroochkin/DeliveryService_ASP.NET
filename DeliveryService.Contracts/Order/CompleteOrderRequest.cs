namespace DeliveryService.Contracts.Order;

public record CompleteOrderRequest(
	string OrderId);

public record CompleteOrderResponse(
	string OrderId);
