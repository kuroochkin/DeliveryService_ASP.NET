using DeliveryService.Contracts.Order.Get;

namespace DeliveryService.Contracts.Product.Get;
public record GetAllProductsResponse(
	List<GetProductDetailsResponse> Products);

public record GetProductDetailsResponse(
	string ProductId,
	string Title,
	string Price,
	string Thumbnail
	);


