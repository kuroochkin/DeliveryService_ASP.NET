namespace DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;

public interface IUnitOfWork
{
	IRestaurantRepository Restaurants { get; }
	IManagerRepository Managers { get; }
	Task<bool> CompleteAsync();
}
