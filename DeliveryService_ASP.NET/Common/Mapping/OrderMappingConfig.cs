using DeliveryService.App.Order.Commands.CheckoutPayment;
using DeliveryService.App.Order.Commands.CreateOrder;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.App.Order.Queries.GetOrdersUser;
using DeliveryService.Contracts.Order;
using DeliveryService.Contracts.Order.Get;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

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
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.Products, src => src.Products);
		

		config.NewConfig<(CreateOrderRequest request, string customerId), CreateOrderCommand>()
			.Map(dest => dest.CustomerId, src => src.customerId)
			.Map(dest => dest.Description, src => src.request.Description)
			.Map(dest => dest.Products, src => src.request.Products);

		config.NewConfig<(CheckoutPaymentRequest request, string customerId), CheckoutPaymentCommand>()
			.Map(dest => dest.CustomerId, src => src.customerId)
			.Map(dest => dest.OrderId, src => src.request.OrderId)
			.Map(dest => dest.CardNumber, src => src.request.CardNumber)
			.Map(dest => dest.Cvv, src => src.request.Cvv)
			.Map(dest => dest.ExpiryMonthYear, src => src.request.ExpiryMonthYear)
			.Map(dest => dest.OrderTotalSum, src => src.request.OrderTotalSum)
			.Map(dest => dest.CartTotalItems, src => src.request.CartTotalItems);


		config.NewConfig<OrdersUserVm, GetOrdersCustomerResponse>()
			.Map(dest => dest.Orders, src => src.Orders);

		config.NewConfig<OrdersUserVm, GetOrdersCourierResponse>()
			.Map(dest => dest.Orders, src => src.Orders);

		config.NewConfig<OrdersUserVm, GetAllOrdersByCreateResponse>()
			.Map(dest => dest.Orders, src => src.Orders);


	}
}
