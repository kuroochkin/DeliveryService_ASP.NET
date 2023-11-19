namespace DeliveryService.Services.CourierAPI.Contracts.Courier;

public record CompleteOrderCourierRequest(
	string CourierId,
	string OrderId);

