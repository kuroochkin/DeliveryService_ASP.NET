namespace DeliveryService.Services.CourierAPI.Contracts.Courier;

public record ConfirmOrderCourierRequest(
	string CourierId,
	string OrderId);

