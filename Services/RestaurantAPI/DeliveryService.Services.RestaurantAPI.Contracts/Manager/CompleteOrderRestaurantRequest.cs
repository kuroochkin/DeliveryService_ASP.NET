namespace DeliveryService.Services.RestaurantAPI.Contracts.Manager;

public record CompleteOrderRestaurantRequest(
	string ManagerId,
	string OrderId);
