namespace DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	ICustomerRepository Customers { get; }
	IOrderRepository Orders { get; }
	IOrderItemRepository OrderItems { get; }

	Task<bool> CompleteAsync();
}
