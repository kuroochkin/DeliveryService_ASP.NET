using DeliveryService.Services.ProductAPI.Domain.Product;

namespace DeliveryService.Services.ProductAPI.App.Common.Interfaces;

public interface IProductRepository : IGenericRepository<ProductEntity>
{
	Task<ProductEntity?> FindProductById(int id);
	Task<List<ProductEntity>?> GetAllProducts();
	Task<List<ProductEntity>?> GetProductsBySectionId(Guid id);
}