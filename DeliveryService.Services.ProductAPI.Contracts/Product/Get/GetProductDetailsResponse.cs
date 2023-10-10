namespace DeliveryService.Services.ProductAPI.Contracts.Product.Get;

public record GetProductDetailsResponse(
	string ProductId,
	string Title,
	string Price,
	string Thumbnail,
	string Section
	);