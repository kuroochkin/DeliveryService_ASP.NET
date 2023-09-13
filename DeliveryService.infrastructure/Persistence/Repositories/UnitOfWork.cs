using DeliveryService.App.Common.Interfaces.Persistence;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public UnitOfWork(ApplicationDbContext context, 
		ICourierRepository couriers, 
		ICustomerRepository customers, 
		IManagerRepository managers,
		IRestaurantRepository restaurants,
		IOrderRepository orders, 
		IProductRepository products,
		IUserRepository users,
		IRoleRepository roles,
		IOrderItemRepository orderItems,
		ISectionRepository sections)
	{
		_context = context;
		Users = users;
		Roles = roles;
		Couriers = couriers;
		Customers = customers;
		Managers = managers;
		Restaurants = restaurants;
		Orders = orders;
		Products = products;
		OrderItems = orderItems;
		Sections = sections;
	}

	public IUserRepository Users { get; }
	public IRoleRepository Roles { get; }
	public ICourierRepository Couriers { get; }
	public ICustomerRepository Customers { get; }
	public IManagerRepository Managers { get; }
	public IRestaurantRepository Restaurants { get; }
	public IOrderRepository Orders { get; }
	public IProductRepository Products { get; }
	public IOrderItemRepository OrderItems { get; }
	public ISectionRepository Sections { get; }
	

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
