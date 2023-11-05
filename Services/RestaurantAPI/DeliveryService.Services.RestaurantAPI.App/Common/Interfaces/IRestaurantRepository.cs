using DeliveryService.Services.RestaurantAPI.Domain.Restaurant;

namespace DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;

public interface IRestaurantRepository : IGenericRepository<RestaurantEntity>
{
	Task<RestaurantEntity?> FindRestaurantByName(string name);
}
