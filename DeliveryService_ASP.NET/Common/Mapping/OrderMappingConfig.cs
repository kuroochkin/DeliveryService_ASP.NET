using DeliveryService.App.Courier.Commands.AddCourier.AddOrder;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer;
using DeliveryService.Contracts.Order;
using DeliveryService.Contracts.Order.Get;
using Mapster;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryService.API.Common.Mapping
{
	public class OrderMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<OrderDetailsVm, GetOrderDetailsResponse>()
				.Map(dest => dest.OrderId, src => src.OrderId)
				.Map(dest => dest.Courier, src => src.Courier)
				.Map(dest => dest.Customer, src => src.Customer)
				.Map(dest => dest.Created, src => src.Created)
				.Map(dest => dest.Status, src => src.Status)
				.Map(dest => dest.Description, src => src.Description);

			config.NewConfig<(CreateOrderRequest request, string customerId), CreateOrderCommand>()
				.Map(dest => dest.CustomerId, src => src.customerId)
				.Map(dest => dest.Description, src => src.request.Description);

			config.NewConfig<ConfirmOrderRequest, ConfirmOrderCommand>()
				.Map(dest => dest.CourierId, src => src.CourierId)
				.Map(dest => dest.OrderId, src => src.OrderId);

			config.NewConfig<OrdersUserVm, GetOrdersCustomerResponse>()
				.Map(dest => dest.Orders, src => src.Orders);

			config.NewConfig<OrdersUserVm, GetOrdersCourierResponse>()
				.Map(dest => dest.Orders, src => src.Orders);
		}
	}
}
