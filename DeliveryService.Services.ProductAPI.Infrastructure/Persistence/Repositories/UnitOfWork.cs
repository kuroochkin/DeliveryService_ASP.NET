using DeliveryService.Services.ProductAPI.App.Common.Interfaces;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context,
		IProductRepository products,
		ISectionRepository sections)
	{
		_context = context;
		Products = products;
		Sections = sections;
	}

	public IProductRepository Products { get; }

	public ISectionRepository Sections { get; }

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
