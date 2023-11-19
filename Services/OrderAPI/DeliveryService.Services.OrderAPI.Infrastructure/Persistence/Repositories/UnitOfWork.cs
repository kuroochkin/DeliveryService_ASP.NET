using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context,
		IOrderRepository orders,
		IOrderItemRepository orderItems,
		ICustomerRepository customers)
	{
		_context = context;
		Orders = orders;
		OrderItems = orderItems;
		Customers = customers;
	}

	public ICustomerRepository Customers { get; }
	public IOrderRepository Orders { get; }
	public IOrderItemRepository OrderItems { get; }


	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
