namespace DeliveryService.Contracts.Order.Get;

public record GetOrdersCustomerResponse(
	List<GetOrderDetailsResponse> Orders);
