namespace DeliveryService.Contracts.Order;

public record ConfirmOrderRequest(
	string CourierId,
	string OrderId);

public record ConfirmOrderResponse(
	string CourierId,
	string OrderId);
