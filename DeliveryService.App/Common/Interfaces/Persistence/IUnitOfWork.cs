namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	ICourierRepository Couriers { get; }
	ICustomerRepository Customers { get; }
	IOrderRepository Orders { get; }
	IProductRepository Products { get; }
	IUserRepository Users { get; }
	ISectionRepository Sections { get; }
	IOrderItemRepository OrderItems { get; }
	IRestaurantRepository Restaraunts { get; }
	IRoleRepository Roles { get; }

	Task<bool> CompleteAsync();
}
