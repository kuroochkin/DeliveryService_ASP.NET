namespace DeliveryService.Services.RestaurantAPI.Contracts.Manager;

public record ConfirmOrderRestaurantRequest(
	string ManagerId,
	string OrderId);
