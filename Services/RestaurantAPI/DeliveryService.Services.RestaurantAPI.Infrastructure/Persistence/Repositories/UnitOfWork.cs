using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context,
		IManagerRepository managers,
		IRestaurantRepository restaurants)
	{
		_context = context;
		Managers = managers;
		Restaurants = restaurants;
	}

	public IManagerRepository Managers { get; }
	public IRestaurantRepository Restaurants { get; }

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
