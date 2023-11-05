using DeliveryService.Services.RestaurantAPI.Domain.Manager;

namespace DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;

public interface IManagerRepository : IGenericRepository<ManagerEntity>
{
	Task<ManagerEntity?> FindManagerWithRestaurantById(Guid id);
}
