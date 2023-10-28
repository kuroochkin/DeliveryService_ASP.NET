namespace DeliveryService.Services.OrderAPI.Contracts;

public record CreateOrderRequest(
	string CustomerId,
	string Description,
	string TotalPrice,
	List<GetProductRequest> Products);

public record CreateOrderResponse(
	string CustomerId,
	string Description,
	string TotalPrice,
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
	string TotalPrice,
	string Thumbnail);

