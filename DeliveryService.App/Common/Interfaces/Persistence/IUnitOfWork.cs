namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	ICourierRepository Couriers { get; }
	ICustomerRepository Customers { get; }
	IOrderRepository Orders { get; }
	IProductRepository Products { get; }

	Task<bool> CompleteAsync();
}
