using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IRestaurantRepository : IGenericRepository<RestaurantEntity>
{
	Task<RestaurantEntity?> FindRestaurantByName(string name);
}
