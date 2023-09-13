using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.App.Order.Commands.CreateOrder;
using Mapster;
using DeliveryService.Contracts.Manager;
using DeliveryService.App.Manager.Commands.JoinRestaurant;

namespace DeliveryService.API.Common.Mapping;

public class ManagerMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(JoinRestaurantRequest request, string managerId), JoinRestaurantCommand>()
			.Map(dest => dest.ManagerId, src => src.managerId)
			.Map(dest => dest.RestaurantId, src => src.request.RestaurantId);
	}
}
