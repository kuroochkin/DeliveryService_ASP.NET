namespace DeliveryService.Contracts.Order;

public record ConfirmOrderRequest(
	string OrderId);

public record ConfirmOrderResponse(
	string OrderId);
