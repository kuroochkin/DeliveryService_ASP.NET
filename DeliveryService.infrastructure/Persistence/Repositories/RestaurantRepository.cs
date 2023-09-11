using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class RestaurantRepository : GenericRepository<RestaurantEntity>, IRestaurantRepository
{
	public RestaurantRepository(ApplicationDbContext context) : base(context)
	{
	}
}
