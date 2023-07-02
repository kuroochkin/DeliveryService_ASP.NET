using DeliveryService.Contracts.Order.Get;

namespace DeliveryService.Contracts.Product.Get;
public record GetAllProductsResponse(
	List<GetProductDetailsResponse> Products);



