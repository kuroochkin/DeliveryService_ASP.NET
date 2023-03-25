using DeliveryService.App.Courier.Commands.AddCourier.AddOrder;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.Contracts.Order;
using DeliveryService.Contracts.Order.Get;
using Mapster;

namespace DeliveryService.API.Common.Mapping
{
	public class OrderMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<OrderDetailsVm, GetOrderDetailsResponse>();

			config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
				.Map(dest => dest.CustomerId, src => src.CustomerId)
				.Map(dest => dest.Description, src => src.Description);

			config.NewConfig<ConfirmOrderRequest, ConfirmOrderCommand>()
				.Map(dest => dest.CourierId, src => src.CourierId)
				.Map(dest => dest.OrderId, src => src.OrderId);
		}
	}
}
