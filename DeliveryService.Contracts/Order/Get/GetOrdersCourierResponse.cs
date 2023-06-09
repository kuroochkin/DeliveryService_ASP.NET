namespace DeliveryService.Contracts.Order.Get;

public record GetOrdersCourierResponse(
	List<GetOrderDetailsResponse> Orders);
