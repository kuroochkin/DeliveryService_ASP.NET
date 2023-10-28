using DeliveryService.Services.OrderAPI.App.Order.Commands.CreateOrder;
using DeliveryService.Services.OrderAPI.Contracts;
using Mapster;

namespace DeliveryService.Services.OrderAPI.Mapping;

public class OrderMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
			.Map(dest => dest.CustomerId, src => src.CustomerId)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.TotalPrice, src => src.TotalPrice)
			.Map(dest => dest.Products, src => src.Products);

		config.NewConfig<GetProductRequest, GetProductResponse>()
			.Map(dest => dest.ProductId, src => src.ProductId)
			.Map(dest => dest.TotalPrice, src => src.TotalPrice)
			.Map(dest => dest.Title, src => src.Title)
			.Map(dest => dest.Thumbnail, src => src.Thumbnail);
	}
}