namespace DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	ICourierRepository Couriers { get; }
	Task<bool> CompleteAsync();
}
