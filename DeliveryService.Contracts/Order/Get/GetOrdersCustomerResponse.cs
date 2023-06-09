namespace DeliveryService.Contracts.Order.Get;

public record GetOrdersCustomerResponse(
	List<GetOrderDetailsResponse> Orders);

public record GetAllOrdersByCreateResponse(
	List<GetOrderDetailsResponse> Orders);

