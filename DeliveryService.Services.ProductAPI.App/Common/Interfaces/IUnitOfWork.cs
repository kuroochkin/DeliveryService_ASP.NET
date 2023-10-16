namespace DeliveryService.Services.ProductAPI.App.Common.Interfaces;

public interface IUnitOfWork
{
	IProductRepository Products { get; }
	ISectionRepository Sections { get; }
	Task<bool> CompleteAsync();
}
