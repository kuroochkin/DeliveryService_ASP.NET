namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	IRoleRepository Roles { get; }
	ICourierRepository Couriers { get; }
	ICustomerRepository Customers { get; }
	IManagerRepository Managers { get; }
	IOrderRepository Orders { get; }
	IProductRepository Products { get; }
	ISectionRepository Sections { get; }
	IOrderItemRepository OrderItems { get; }
	IRestaurantRepository Restaurants { get; }

	Task<bool> CompleteAsync();
}
