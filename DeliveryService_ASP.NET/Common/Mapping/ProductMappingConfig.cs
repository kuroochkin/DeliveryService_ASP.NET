using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.App.Order.Queries.GetOrdersUser;
using DeliveryService.App.Product.Queries;
using DeliveryService.Contracts.Order.Get;
using DeliveryService.Contracts.Product.Get;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class ProductMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ProductDetailsVm, GetProductDetailsResponse>()
			.Map(dest => dest.ProductId, src => src.ProductId)
			.Map(dest => dest.Title, src => src.Title)
			.Map(dest => dest.Price, src => src.Price)
			.Map(dest => dest.Thumbnail, src => src.Thumbnail);

		config.NewConfig<ProductsVm, GetAllProductsResponse>()
			.Map(dest => dest.Products, src => src.Products);
	}
}
