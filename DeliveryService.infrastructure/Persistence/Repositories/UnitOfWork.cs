using DeliveryService.App.Common.Interfaces.Persistence;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context, 
		ICourierRepository couriers, 
		ICustomerRepository customers, 
		IOrderRepository orders, 
		IProductRepository products,
		IUserRepository users,
		IOrderItemRepository orderItems,
		ISectionRepository sections,
		IStorageFileRepository storageFiles)
	{
		_context = context;
		Couriers = couriers;
		Customers = customers;
		Orders = orders;
		Products = products;
		Users = users;
		OrderItems = orderItems;
		Sections = sections;
		StorageFiles = storageFiles;
	}

	public ICourierRepository Couriers { get; }
	public ICustomerRepository Customers { get; }
	public IOrderRepository Orders { get; }
	public IProductRepository Products { get; }
	public IUserRepository Users { get; }
	public IOrderItemRepository OrderItems { get; }
	public ISectionRepository Sections { get; }
	public IStorageFileRepository StorageFiles { get; }

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
