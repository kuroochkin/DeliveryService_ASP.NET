namespace DeliveryService.Services.OrderAPI.Contracts.Order;

public record CreateOrderRequest(
	string Description,
	List<GetProductRequest> Products);

public record CreateOrderResponse(
	string Description,
	List<GetProductResponse> Products);

public record GetProductRequest(
	string ProductId,
	string TotalPrice,
	string Count,
	string Thumbnail,
	string Title);

public record GetProductResponse(
	string ProductId,
	string Title,
	string Price,
	string Thumbnail);
