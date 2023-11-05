using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;
using DeliveryService.Services.RestaurantAPI.Domain.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.Repositories;

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
