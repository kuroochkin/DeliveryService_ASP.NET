using Mapster;
using DeliveryService.Contracts.Manager;
using DeliveryService.App.Manager.Commands.JoinRestaurant;
using DeliveryService.App.Order.Commands.ConfirmOrderRestaurant;
using DeliveryService.App.Order.Commands.EndOrderRestaurant;

namespace DeliveryService.API.Common.Mapping;

public class ManagerMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(JoinRestaurantRequest request, string managerId), JoinRestaurantCommand>()
			.Map(dest => dest.ManagerId, src => src.managerId)
			.Map(dest => dest.RestaurantId, src => src.request.RestaurantId);

		config.NewConfig<(ConfirmOrderRestaurantRequest request, string managerId), ConfirmOrderRestaurantCommand>()
			.Map(dest => dest.ManagerId, src => src.managerId)
			.Map(dest => dest.OrderId, src => src.request.OrderId);

		config.NewConfig<(EndOrderRestaurantRequest request, string managerId), EndOrderRestaurantCommand>()
			.Map(dest => dest.ManagerId, src => src.managerId)
			.Map(dest => dest.OrderId, src => src.request.OrderId);
	}
}
