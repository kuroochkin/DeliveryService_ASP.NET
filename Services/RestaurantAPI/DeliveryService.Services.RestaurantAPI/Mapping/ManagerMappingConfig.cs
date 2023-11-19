using DeliveryService.Services.RestaurantAPI.App.Manager.Commands.JoinRestaurant;
using DeliveryService.Services.RestaurantAPI.Contracts.Manager;
using Mapster;

namespace DeliveryService.Services.RestaurantAPI.Mapping;

public class ManagerMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(JoinRestaurantRequest request, string managerId), JoinRestaurantCommand>()
			.Map(dest => dest.ManagerId, src => src.managerId)
			.Map(dest => dest.RestaurantId, src => src.request.RestaurantId);
	}
}
