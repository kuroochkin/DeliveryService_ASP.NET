namespace DeliveryService.Services.ProductAPI.Contracts.Product.Get;

public record GetAllProductsResponse(
	List<GetProductDetailsResponse> Products);
