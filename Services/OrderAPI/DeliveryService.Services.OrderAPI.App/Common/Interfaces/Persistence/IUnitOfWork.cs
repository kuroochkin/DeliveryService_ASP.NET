namespace DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	IOrderRepository Orders { get; }
	IOrderItemRepository OrderItems { get; }

	Task<bool> CompleteAsync();
}
