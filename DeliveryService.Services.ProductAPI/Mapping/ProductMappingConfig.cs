using DeliveryService.Services.ProductAPI.App.Vm.Product;
using DeliveryService.Services.ProductAPI.Contracts.Product.Get;
using Mapster;

namespace DeliveryService.Services.ProductAPI.Mapping;

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