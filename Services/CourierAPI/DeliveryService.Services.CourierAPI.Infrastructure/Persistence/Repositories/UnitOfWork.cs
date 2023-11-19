using DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;

namespace DeliveryService.Services.CourierAPI.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context,
		ICourierRepository couriers)
	{
		_context = context;
		Couriers = couriers;
	}

	public ICourierRepository Couriers { get; }

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
