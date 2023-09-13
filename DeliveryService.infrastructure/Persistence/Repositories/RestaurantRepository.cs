using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Restaraunt;
using DeliveryService.Domain.Role;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class RestaurantRepository : GenericRepository<RestaurantEntity>, IRestaurantRepository
{
	public RestaurantRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<RestaurantEntity?> FindRestaurantByName(string name)
	{
		return await _context.Restaurants
				.FirstOrDefaultAsync(role => role.Name == name);
	}
}
